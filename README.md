# UDP Communication Interface Asset for Intuiface

**Important**: This Interface Asset is only available for Player for Windows. 

The UDP Communication enables to you send or receive message over UDP. 
It is composed of 

* 2 read-only properties
   * Last message received
   * Logs
* 1 trigger
   * Message received. This trigger is raised with 1 parameter: Message. 
* 3 actions: 
   * Start listening: You need to call this action to start listening on a dedicated UDP port
   * Send message: sends an ASCII message to specified address and port
   * Send hex message: sends an Hexadecimal message to specified address and port
   
## How to use the UDP Communication Interface Asset

To add a Face UDP Communication Interface Asset into an Intuiface experience, follow these steps:

* Close all running instances of **Intuiface Composer**.
* Download the [latest released package here](https://github.com/intuiface/UDPCommunicationIA/releases).
* Extract the archive and copy the **UDPCommunicationIA** folder to the path "[Drive]:\Users\\[UserName]\Documents\Intuiface\Interface Assets".
* Launch **Intuiface Composer** and open your project.
* Open the Interface Asset panel and select the **Add an Interface Asset** option. When you enter "UDP" in the search bar, you should see the **UDPCommunication** Interface Asset.

## License

Copyright © 2020 Intuiface.

Released under the MIT License.
