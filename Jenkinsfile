pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building...'
                sh 'dotnet build'
            }
        }

        stage('Test') {
            steps {
                echo 'Testing...'
                sh 'dotnet test'
            }
        }

        stage('Deploy to Beta') {
            when {
                branch 'develop'
            }

            steps {
                echo 'Packaging...'
                sh 'dotnet publish -c Release -r linux-x64'

                echo 'Deploying to Beta...'
                sshPublisher(
                    publishers: [
                        sshPublisherDesc(
                            configName: 'd001',
                            transfers: [
                                sshTransfer(
                                    sourceFiles: '''
                                        src/App/bin/Release/netcoreapp3.1/linux-x64/publish/**
                                        etc/**
                                    ''',
                                    patternSeparator: '[, \\r\\n]+',
                                    remoteDirectory: 'aspnet-main',
                                    cleanRemote: true,
                                    execCommand: '''
                                        chmod +x aspnet-main/src/App/bin/Release/netcoreapp3.1/linux-x64/publish/App
                                        sh aspnet-main/etc/beta/bin/aspnet-main-web.restart
                                    ''',
                                    usePty: true,
                                )
                            ]
                        )
                    ]
                )
            }
        }

        stage('Deploy to Real') {
            when {
                branch 'master'
            }

            steps {
                echo 'Packaging...'
                sh 'dotnet publish -c Release -r linux-x64'

                echo 'Deploying to Real...'
            }
        }
    }

    post {
        success {
            slackSend color: 'good',  message: "Build Success: ${env.JOB_NAME} <${env.BUILD_URL}|#${env.BUILD_NUMBER}>"
        }

        failure {
            slackSend color: 'danger', message: "Build Failed: ${env.JOB_NAME} <${env.BUILD_URL}|#${env.BUILD_NUMBER}>"
        }
    }
}
