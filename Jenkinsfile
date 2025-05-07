pipeline {
    agent any

    triggers {
        githubPush() // GitHub push tetikleyicisi
    }

    stages {
        stage('Checkout') {
            steps {
                git url: 'https://github.com/JegBaha/YazilimProje.git', branch: 'master', credentialsId: '7112aeee-81fa-4d0e-9542-ed1518e4be10'
                // Debug: List workspace contents
                bat 'dir "${WORKSPACE}"'
            }
        }

        stage('Build') {
            steps {
                script {
                    // UTF-8 karakter kodlamasına geçiş
                    bat 'chcp 65001'

                    // Çalışma dizinini yazdırma
                    bat 'echo %cd%'

                    // Solution dosyasını kontrol etme
                    bat 'if exist "${WORKSPACE}\\YazilimProje.sln" (echo Solution file exists) else (echo Solution file does not exist)'

                    // MSBuild ile build
                    def msBuildPath = '"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe"'
                    def solutionFile = '"${WORKSPACE}\\YazilimProje.sln"'
                    bat "${msBuildPath} ${solutionFile} /p:Configuration=Release"
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    // Test proje dosyasını kontrol etme
                    def testProjectPath = "${WORKSPACE}\\YazilimProje.Tests\\YazilimProje.Tests.csproj"
                    bat 'if exist "${testProjectPath}" (echo Test project exists) else (echo Test project does not exist)'

                    // Test sonuçları dizinini oluşturma
                    def resultsDirectory = "${WORKSPACE}\\TestResults"
                    bat 'mkdir "${resultsDirectory}" || echo Results directory already exists'

                    // dotnet test ile testleri çalıştırma, verbose çıktı için --verbosity detailed
                    bat "dotnet test \"${testProjectPath}\" --logger \"trx;LogFileName=TestResults.trx\" --results-directory \"${resultsDirectory}\" --verbosity detailed"
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    echo 'Deploying application...'

                    // Çalıştırılabilir dosyanın var olup olmadığını kontrol etme
                    def exeFile = "${WORKSPACE}\\YazilimProje\\bin\\Release\\YazilimProje.exe"
                    if (fileExists(exeFile)) {
                        echo "Executable exists: ${exeFile}"
                        // Uygulamayı yerel olarak çalıştırma
                        bat "start \"\" \"${exeFile}\""
                    } else {
                        echo "Executable not found: ${exeFile}"
                    }
                }
            }
        }
    }

    post {
        always {
            script {
                archiveArtifacts artifacts: '**/bin/**/*.*', allowEmptyArchive: true

                // TRX dosyasının varlığını kontrol etme
                def trxFile = "${WORKSPACE}\\TestResults\\TestResults.trx"
                if (fileExists(trxFile)) {
                    try {
                        mstest testResultsFile: 'TestResults\\TestResults.trx'
                    } catch (Exception e) {
                        echo "Failed to process MSTest results: ${e.getMessage()}"
                    }
                } else {
                    echo 'No TRX test results found.'
                }
            }
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