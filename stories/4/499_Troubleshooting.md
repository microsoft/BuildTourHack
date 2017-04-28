# Task 4 Troubleshooting Guide

## "Firewall detected" when sharing drive

If you encounter a firewall error when sharing drives with Docker, you may need to reset Windows File and Printer sharing on your Hyper-V virtual switch.

1. Right click your network connection in the taskbar, and choose **Network and Sharing Center**
2. In the left menu, select **Change Adapter Settings**
3. Right click and open the properties for the "vEthernet (DockerNAT)" adapter
4. Uncheck the box for **File and Printer Sharing for Windows** and hit OK
![](images/troubleshooting-fileshare.png)
5. Open the properties again, re-enable **File and Printer Sharing for Windows** and hit OK
6. You should now be able to share drives with Docker