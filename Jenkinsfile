pipeline {
  agent {
    node {
      label 'sapin3'
    }

  }
  stages {
    stage('Build') {
      steps {
        bat "\"${tool 'msbuild-v15'}\\MSBuild.exe\" /v:m /clp:ErrorsOnly;Summary /p:Configuration=Release /p:Platform=\"Any CPU\" source/UDPCommunication/UDPCommunication.sln"
        
      }
    }

    // stage('Sign') {
    //   steps {
		// copyArtifacts filter: '**/Cryptifix-x64-*.zip', fingerprintArtifacts: true, projectName: 'IntuiFace/master', selector: lastSuccessful(), target: 'cryptifix'
		// script {
		// 	def files = findFiles(glob: '**/Cryptifix-x64-*.zip')
		// 	unzip zipFile: "cryptifix\\${files[0].name}", dir: 'cryptifix'
		// }
		// bat "cryptifix\\Cryptifix.exe sign \"dist\\x64\\Release\\InterfaceAssetTemplate\" --LicenseEdition=FREE --IsAllowedByNonInteractivePlayer=false"
    //     callBuildScript workspace: workspace, script:'signcode.pl', params: "\"OpenVINOFaceDetectionServer\" \"dist\\x64\\Release\\OpenVINOFaceDetectionServer\\*.exe\""
       }
    }

    stage('Package') {
      steps {		
		zip archive: true, dir: "source\\UDPCommunication\\UDPCommunication\\bin\\Release", glob: '', zipFile: "UDPCommunication.zip"
      }
    }
	
    stage('Archive') {
      steps {        
        archiveArtifacts 'UDPCommunication.zip'
      }
    }

  }
}