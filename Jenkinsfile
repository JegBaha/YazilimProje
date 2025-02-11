pipeline {
    agent any

    triggers {
        githubPush() // GitHub push tetikleyicisi
    }

    stages {
        stage('Checkout') {
            steps {
                git url: 'https://github.com/JegBaha/YazilimProje.git', branch: 'master', credentialsId: '7112aeee-81fa-4d0e-9542-ed1518e4be10'
            }
        }

        stage('Build') {
            steps {
                script {
                    // UTF-8 karakter kodlamasına geçiş
                    bat 'chcp 65001'

                    // Çalışma dizinini yazdırma
                    bat 'echo %cd%'

                    // Dosya yolunu kontrol etme
                    bat 'if exist "C:\\Users\\bahab\\source\\repos\\YazilimProje\\YazilimProje.sln" (echo File exists) else (echo File does not exist)'

                    def msBuildPath = '"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe"'
                    def solutionFile = '"C:\\Users\\bahab\\source\\repos\\YazilimProje\\YazilimProje.sln"'
                    bat "${msBuildPath} ${solutionFile} /p:Configuration=Release"
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    def testProjectPath = 'C:\\Users\\bahab\\source\\repos\\YazilimProje\\YazilimProje.Tests\\YazilimProje.Tests.csproj'
                    def resultsDirectory = 'C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\YazilimProje\\TestResults'
                    bat "dotnet test \"${testProjectPath}\" --logger \"junit;LogFileName=TestResults.xml\" --results-directory \"${resultsDirectory}\""
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    // Deploy adımlarınızı buraya ekleyin
                    echo 'Deploying application...'
                }
            }
        }
    }

    post {
        always {
            archiveArtifacts artifacts: '**/bin/**/*.*', allowEmptyArchive: true
            junit 'C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\YazilimProje\\TestResults\\TestResults.xml'
        }
        success {
            echo 'Build ve testler başarılı!'
        }
        unstable {
            echo 'Build tamamlandı fakat bazı testler başarısız oldu.'
        }
        failure {
            echo 'Build veya testler başarısız oldu.'
        }
    }
}