terraform {
  required_providers {
    aws = {
        source = "hashicorp/aws",
        version =  "~> 5.31"
    }

  }

  required_version = ">= 1.2.0"

    backend "s3" {
        bucket         	   = "lyrics-game-terraform-state-bucket"
        key                = "state/terraform.tfstate"
        region         	   = "us-east-1"
        encrypt        	   = true
        # dynamodb_table     = "mycomponents_tf_lockid"
    }
}

provider "aws" {
    region  = "us-east-1"
}


resource "aws_lambda_function" "lyrics_game_lambda" {
    architectures                  = [
        "x86_64",
    ]
    function_name                  = "lyrics-game-lambda"
    handler                        = "lyrics-game-lambda::lyrics_game_lambda.Function::FunctionHandler"
    layers                         = []
    memory_size                    = 256
    package_type                   = "Zip"
    reserved_concurrent_executions = -1
    role                           = "arn:aws:iam::915898657279:role/service-role/lyrics-game-lambda-role-5enu4vjm"
    runtime                        = "dotnet6"
    skip_destroy                   = false
    tags                           = {}
    tags_all                       = {}
    timeout                        = 30
    filename                       = "../src/lyrics-game-lambda/bin/Release/net6.0/lyrics-game-lambda.zip"

    ephemeral_storage {
        size = 512
    }

    tracing_config {
        mode = "PassThrough"
    }
}

resource "aws_api_gateway_rest_api" "lyrics_game_api" {
    api_key_source               = "HEADER"
    binary_media_types           = []
    disable_execute_api_endpoint = false
    name                         = "lyrics-game-api"
    put_rest_api_mode            = "overwrite"
    tags                         = {}
    tags_all                     = {}

    endpoint_configuration {
        types            = [
            "REGIONAL",
        ]
    }
}

resource "aws_api_gateway_resource" "get_resource" {
  rest_api_id = aws_api_gateway_rest_api.lyrics_game_api.id
  parent_id   = aws_api_gateway_rest_api.lyrics_game_api.root_resource_id
  path_part   = "get"  // The path for this resource
}

resource "aws_api_gateway_method" "get_any"{
    rest_api_id = aws_api_gateway_rest_api.lyrics_game_api.id
    authorization = "NONE"
    resource_id = aws_api_gateway_resource.get_resource.id
    http_method = "ANY"
}

resource "aws_api_gateway_method" "get_option"{
    rest_api_id = aws_api_gateway_rest_api.lyrics_game_api.id
    authorization = "NONE"
    resource_id = aws_api_gateway_resource.get_resource.id
    http_method = "OPTIONS"
}

resource "aws_api_gateway_integration" "lambda_gateway_integration"{
    content_handling        = "CONVERT_TO_TEXT"
    rest_api_id             = aws_api_gateway_rest_api.lyrics_game_api.id
    resource_id             = aws_api_gateway_resource.get_resource.id
    type                    = "AWS"
    http_method             = "ANY"
    integration_http_method = "POST"
    passthrough_behavior    = "WHEN_NO_MATCH"
    request_parameters      = {}
    request_templates       = {}
    timeout_milliseconds    = 29000
    uri                     = aws_lambda_function.lyrics_game_lambda.invoke_arn

}