pipeline {
    agent any // Runs on any agent

    environment {
        AZURE_FUNCTIONAPP_PACKAGE_PATH = 'MyHelloWorldFuncApp' // Path to the function app project
        DOTNET_VERSION = '8.0.407' // .NET version

        RESOURCE_GROUP = 'rg_azjenki' // Azure Resource Group
        FUNCTION_APP_NAME = 'MyHelloWorldFuncApp' // Azure Function App Name
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/prabhgill02/jenkins-azfunc-ps'
            }
        }

        stage('Setup .NET') {
            steps {
                script {
                    bat "choco install dotnet-sdk --version=${DOTNET_VERSION} -y"
                }
            }
        }

        stage('Build') {
            steps {
                script {
                    dir(env.AZURE_FUNCTIONAPP_PACKAGE_PATH) {
                        bat "dotnet build --configuration Release --output ../output"
                    }
                }
            }
        }
        
        stage('Test') {
            steps {
                script {
                    dir('MyHelloWorldFunctApp.Tests') {
                        bat "dotnet test --configuration Release"
                    }
                }
            }
        }

        stage('Create Deployment Package') {
            steps {
                script {
                    bat '''
                        cd output
                        powershell Compress-Archive -Path * -DestinationPath ../output.zip -Force
                    '''
                }
            }
        }
    

        stage('Deploy to Azure Function') {
            steps {
                script {
                    bat '''
                        rem Azure CLI login using service principal
                        az login --service-principal --username %AZURE_CLIENT_ID% --password %AZURE_CLIENT_SECRET% --tenant %AZURE_TENANT_ID%
                        
                        rem Set Azure subscription (if required)
                        az account set --subscription "AZURE_SUBSCRIPTION_ID"

                        rem Deploy function app using zip
                        az functionapp deployment source config-zip --name %FUNCTION_APP_NAME% --resource-group %RESOURCE_GROUP% --src output.zip
                    '''
                }
            }
        }
    }

    post {
        success {
            echo 'Deployment successful!'
        }
        failure {
            echo 'Deployment failed!'
        }
    }
}
