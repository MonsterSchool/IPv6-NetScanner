<a>
    <img src="/src/Resources/load.gif" alt="Logo" title="IPv6-NetScanner" align="right" height="60" />
</a>

# IPv6 Network Scanner
This is a fast, simple and super lightweight IPv6 network scanner with graphical interface written with C#, [SharpPcap](https://github.com/chmorgan/sharppcap) and [PacketNet](https://github.com/chmorgan/packetnet).

## Download IPv6 Network Scanner
**Make sure you have [.NET 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework) and [Npcap](https://nmap.org/download.html) installed!**<br>
You can download the already compiled IPv6 scanner [here](https://github.com/MonsterSchool/IPv6-NetScanner/releases/download/v.1.0.2.1/IPv6-NetScanner.exe) (releases). 

## Screenshot
<img src="img/01.JPG">

## The Networkscan
NetScanner sends four ICMPv6 Echo-Request messages over the specified network card. The following scopes are used:
```
- FF01::1 (Interface-local all Nodes)
- FF01::2 (Interface-local all Routers)
- FF02::1 (Link-local all Nodes)
- FF02::2 (Link-local all Routers)
```
The entire scanning process takes a maximum of 8 seconds. Depending on the response, the different interfaces are assigned to the scopes.

## MAC-Address Vendor Database
The Database-File can be downloaded here: [Link](https://maclookup.app/downloads/cisco-vendor-macs-xml-database) 
## Changelog
- V 0.9.2.1 - Project start. Implemented basic IP & MAC-Discovery.
- V 1.0.2.1 and later - (Please have a look at the pre-release to see the changes)

## Copyright
The contents and works in this software created by the software operators are subject to German copyright law. The reproduction, editing, distribution and any kind of use outside the limits of copyright law require the written consent of the respective author or creator. Downloads and copies of this software are only permitted for private, non-commercial use.

Insofar as the content on this software was not created by the operator, the copyrights of third parties are observed. In particular, third-party content is identified as such. Should you nevertheless become aware of a copyright infringement, please inform us accordingly. If we become aware of any infringements, we will remove such contents immediately.

Source: [eRecht24.de](https://www.e-recht24.de/)
Cheers 👀
