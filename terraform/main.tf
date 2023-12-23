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
    source_code_hash               = "RsNuSKTTq+wqxzv10l4qvtn0Az2bubmkkjGh5+1/YH8="
    tags                           = {}
    tags_all                       = {}
    timeout                        = 30
    filename                       = "./publish/lyrics-game-lambda.zip"

    ephemeral_storage {
        size = 512
    }

    tracing_config {
        mode = "PassThrough"
    }
}

