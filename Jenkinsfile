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
	//Missing Sign step

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
