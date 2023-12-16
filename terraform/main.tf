terraform {
  required_providers {
    aws = {
        source = "hashicorp/aws",
        version =  "~> 5.31"
    }
  }

  required_version = ">= 1.2.0"
}

provider "aws" {
    region  = "us-east-1"
}


resource "aws_lambda_function" "lyrics_game_lambda" {
    function_name = "lyrics-game-lambda"
    role = "arn:aws:iam::915898657279:role/service-role/lyrics-game-lambda-role-5enu4vjm"
    runtime = "dotnet6"
    handler = "lyrics-game-lambda::lyrics_game_lambda.Function::FunctionHandler"
    memory_size      = 256
    timeout          = 30
    filename = "../src/lyrics-game-lambda/Release/net6.0/lyrics-game-lambda.zip"
}