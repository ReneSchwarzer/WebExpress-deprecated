![WebExpress logo](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/icon.png)

# General
WebExpress is a lightweight web server that has been optimized for use in low-performance environments. Even on 
small systems, such as the Raspberry PI, web applications can be operated efficiently. This is achieved through a 
small footprint with a low resource burden. Furthermore, WebExpress has a powerful and optimized plugin system, with a 
comprehensive API and application templates. This allows web applications to be easily and quickly integrated into a . Net language 
(e.g. C#). WebExpress is based on Kestrel, a cross-platform web server for ASP.NET core. With this, WebExpress also supports:

- https
- HTTP/2 (currently not macOS)

# License
The software is freely available as open source (MIT). The software sources can be obtained 
from https://github.com/ReneSchwarzer/WebExpress. WebExpress is based on components that are 
available as open source:

- https://github.com/dotnet/core (MIT)
- https://getbootstrap.com/ (MIT)
- https://www.chartjs.org (MIT)
- https://jquery.com/ (MIT)
- https://summernote.org/ (MIT)
- https://popper.js.org/ (MIT)
- https://github.com/kurtobando/simple-tags (MIT)
- https://github.com/uxsolutions/bootstrap-datepicker (Apache 2.0)

```
The MIT License (MIT)

Copyright (c) 2023 René Schwarzer

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

# Installation
The installation is described using the Raspberry PI. However, the general procedure can also be applied to 
other operating systems.

## Installing the operating system
The first step is to write the operating system to an SD card. For this purpose, there is https://downloads.raspberrypi.org/imager/imager.exe 
a free program (Windows), with the help of which the image is copied to the SD card.

![imager](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/imager.png)

## Setting up the operating system

In the second step, the SD card is inserted into the Raspberry Pi and the Raspberry Pi is started. Since SSH is not 
yet active, a keyboard and a monitor must be connected. When the Raspberry Pi has been booted, logging in can be done 
with the following data:

```
User: pi 
Passwort: raspberry
```

After successful login, the ```raspi-config``` utility is called, with the help of which the basic configuration of the 
Raspberry Pi is carried out.

```
pi@raspberrypi:~ $ sudo raspi-config
```

It is recommended to change the password, as well as to set up the Wi-Fi, change the time zone and the host name if necessary. In the 
remainder of the application guide, the host name ```wx``` is used. In addition, SSH must be activated (to be found under Interface Options).

![raspiconfig](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/raspiconfig.png)

All subsequent steps can now be done via SSH and the Raspberry Pi can be disconnected from the keyboard and screen.

## Installing the .NET Runtime
After SSH has been activated, a connection to the Raspberry Pi can be established with the help of an SSH client (e.g. Putty, OpenSSH).

![piconnect](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/piconnect.png)

First, the .NET Runtime and the ASP.NET Core Runtime must be installed. Help for this is offered under [1]. The current versions 
can be obtained free of charge from Microsoft at https://dotnet.microsoft.com/download/dotnet-core.

![downloadnet1](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/downloadnet1.png)

For the Raspberry Pi, the binaries for Linux-Arm32 are to be used. The direct link to the Linux-Arm32 binaries must be copied.

![downloadnet2](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/downloadnet2.png)

The Linux Arm32 archive for the ASP.NET Core Runtime is downloaded to the Raspberry using wget.

``` bash
pi@wx:~ $ wget https://download.visualstudio.microsoft.com/download/pr/61cb6649-f41f-4966-84ae-9ff673528054/9bbd07607c5a1af87354e1fa93c36a00/aspnetcore-runtime-7.0.0-linux-arm.tar.gz 
```

In preparation for the installation of .NET Core, a directory must be created at ```/usr/share/dotnet-sdk``` by then unpacking the .NET archive.

``` bash
pi@wx:~ $ sudo mkdir /usr/share/dotnet-sdk 
```

After creating the directory ```/usr/share/dotnet-sdk```, the binaries can be unpacked.

``` bash
pi@wx:~ $ sudo tar zxf aspnetcore-runtime-7.0.0-linux-arm.tar.gz -C /usr/share/dotnet-sdk/
```

## Installing utilities
In the following step, further (service) programs are installed, which are helpful for the execution of WebExpress or for the administration 
of the Raspberry Pi.

As an optional application, the Midnight Commander (MC) can be installed and the profile can be customized.

```
pi@wx:~ $ sudo apt-get install mc -y
```

If necessary, the profile can be extended by ```alias ll='ls -l'```.

![profile](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/profile.png)

## Set static IP
It is recommended to configure a static IP address for the Raspberry under ```/etc/dhcpcd.conf``` (see [2]).

![dhcpcd](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/dhcpcd.png)

## Multicast Domain Name Service (mDNS)
For example, Avahi can be used as mDNS. Avahi is an open-source mDNS implementation. At the command prompt, type the following command to install Avahi:

```
pi@wx:~ $ sudo apt install avahi-daemon -y
```

Once the installation process is complete, local network queries are accepted and answered at ```wx.local```.

## Installing WebExpress
WebExpress is provided in packaged form for the Raspberry Pi in the GitHub repository https://github.com/ReneSchwarzer/WebExpress/releases 
free of charge.

![downloadwebexpress](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/downloadwebexpress.png)

The binaries of WebExpress can be obtained from GitHub via wget.

``` bash
pi@wx:~ $ wget https://github.com/ReneSchwarzer/WebExpress/releases/download/1.4.4.0/WebExpress_1.4.4.0_LiLinuxA32.zip
```

In preparation for the installation of WebExpress, a directory must be created under ```/opt/wx``` by unpacking the binaries.

``` bash
pi@wx:~ $ sudo mkdir /opt/wx 
```

The archive must then be unpacked.

``` bash
pi@wx:~ $ sudo unzip WebExpress_1.4.4.0_LinuxArm32.zip -d /opt/wx 
```

After WebExpress has been successfully unpacked, the execution rights must be granted.

``` bash
pi@wx:~ $ sudo chmod +x /opt/wx/webexpress.sh /opt/wx/WebExpress.App
```

To start the WebExpress application automatically, the supplied SystemCtl unit must be installed.

``` bash
pi@wx:~ $ sudo cp /opt/wx/webexpress.service /etc/systemd/system
```

Finally, the SystemCtl unit must be activated.

``` bash
pi@wx:~ $ sudo systemctl enable webexpress.service
```

# Setting up WebExpress
Before WebExpress can be started, it must be configured. Furthermore, the desired web applications must be installed. 

## Basic configuration
The configuration file ```/opt/wx/config/webexpress.config.xml``` stores the general settings of WebExpress.

|Property         |Description                                                                                      |Example
|-----------------|-------------------------------------------------------------------------------------------------|---
|Endpoint         |Creates an endpoint on which WebExpress listens and processes incoming connections. Any number of endpoints can be configured. -uri: The Uri with the scheme, a hostname, and a port. The hostname * represents all available endpoints. If no port is specified, the default port is used (e.g. 443 for Https). -pfx: The keystore in the form of a pfx file -password: The password of the pfx file. |```<endpoint uri=http://*/ /><endpoint uri="https://*:443/" pfx="./Cert/wx.pfx" password="hello" />```
|Limit            |Sets limits of the web server. connectionlimit: Number of concurrently active connections. Upload limit: The maximum number of bytes that may be transferred to the web server in the body. |```<limit><connectionlimit>300</connectionlimit><uploadlimit>30000000</uploadlimit></limit>```
|Culture          |Specifies the language, calendar used, and formatting for dates and numbers for expenses.        |```<culture>de-DE</culture>```
|Assets directory |Contains static files that are to be served by the web server.                                   |```<assets>./</assets>```
|Context path     |The context path is the prefix path of a Uri (e.g. http://localhost/contextpath/pathToResource). |```<contextpath>wx</contextpath>```
|Plugin directory |Directory where the plugins are executed.                                                        |```<packages>./</packages>```

## Setting up https
Secure and confidential communication between the WebClient and the web server can be guaranteed by using certificates. In the simplest 
case, these certificates can be issued by yourself and installed on the Raspberry Pi. Further information can be found at [3].

### Create a Certificate Authority (CA)
To issue certificates, a Certificate Authority (CA) authority must first be established.

#### Private key
The first thing to do is to create a CA secret private key. This will be named ```caKey.pem```.

``` bash
pi@wx:~ $ openssl genrsa -out caKey.pem 4096
```

#### Create a root certificate
The second step is to create the root certificate, which is named ```ca.pem```.

``` bash
pi@wx:~ $ openssl req -x509 -new -nodes -extensions v3_ca -key caKey.pem -days 36500 -out ca.pem -sha512
```

During the creation process, various data is queried and stored in the root certificate.

|Attribute                |Description                                         |Example
|-------------------------|----------------------------------------------------|-------------
|Country Name             |The country code in two-digit ISO format            |DE
|State or Province Name   |Enter your state here.                              |Berlin
|Locality Name            |Enter your city here. This field specifies the city where the organization is located. Don't use abbreviations. For example, write "Saint Louis" instead of "St. Louis". The field must contain the name of the city in which it is registered. |Berlin
|Organization Name        |Enter your name or company name here. The name of the organization (corporation, limited partnership, university, or government agency) must be registered with an agency at the national, state, or city level.|WebExpress
|Organizational Unit Name |Enter your department (if any) here.                |
|Common Name              |Enter the exact domain name to be protected by the certificate. The common name, also known as the URL, is the fully qualified domain name used for your server's DNS lookup (for example, www.mydomain.com). Browsers use this information to identify your website. Note: You cannot use special characters (?, $,%, etc.), IP addresses, port numbers, or "http:// or https://" in your common name. |WebExpress CA
|Email Address            |Enter the e-mail address of the person responsible. |ca@example.com

Once this step is complete, the CA is ready and ready for use.

### Issue certificate
After the CA is created, it can be used to issue certificates.

#### Private key
A new secret private key must be created for each certificate. For WebExpress, a secret private key named ```wxKey.pem``` is created.

``` bash
pi@wx:~ $ openssl genrsa -out wxKey.pem 4096
```

#### Create a certificate request
To issue a certificate, a certificate request must be made to the CA. The request is stored under the name ```wx.csr```.

``` bash
pi@wx:~ $ openssl req -new -key wxKey.pem -out wx.csr -sha512
```

In the creation process, various data is queried and stored in the request.

|Attribute                |Description                                         |Example
|-------------------------|----------------------------------------------------|------------
Country Name              |The country code in two-digit ISO format            |DE
|State or Province Name   |Enter your state here.                              |Berlin
|Locality Name            |Enter your city here. This field specifies the city where the organization is located. Don't use abbreviations. For example, write "Saint Louis" instead of "St. Louis". The field must contain the name of the city in which it is registered. |Berlin
|Organization Name        |Enter your name or company name here. The name of the organization (corporation, limited partnership, university, or government agency) must be registered with an agency at the national, state, or city level. |WebExpress
|Organizational Unit Name |Enter your department (if any) here.                |
|Common Name              |Enter the exact domain name to be protected by the certificate. The common name, also known as the URL, is the fully qualified domain name used for your server's DNS lookup (for example, www.mydomain.com). Browsers use this information to identify your website. Note: You cannot use special characters (?, $,%, etc.), IP addresses, port numbers, or "http:// or https://" in your common name. |wx.local
|Email Address            |Enter the e-mail address of the person responsible. |wx@example.com 
|A challenge password     |Optional                                            |
|An optional company name |Optional                                            |

#### Issue certificate
After the certificate request has been created, it can be processed by the CA. The certificate is named ```wx.pem```.

``` bash
pi@wx:~ $ openssl x509 -req -in wx.csr -CA ca.pem -CAkey caKey.pem -CAcreateserial -out wx.pem -days 36500 -sha512
```

#### Create a PFX file
For WebExpress, a PFX file ```wx.pfx``` is required, which contains the certificate chain.

``` bash
pi@wx:~ $ openssl pkcs12 -export -out wx.pfx -inkey wxKey.pem -in wx.pem -certfile ca.pem
```

When executing the command, a password must be assigned. Finally, check if the pfx file is correct.

``` bash
pi@wx:~ $ openssl pkcs12 -info -in wx.pfx
```

### Installing certificates in WebExpress
For an https connection, the pfx file (```wx.pfx```) is required. This serves as a certificate store by containing all relevant certificates. This must 
be transferred to the web server and stored in the ```/opt/wx/ssl``` directory in the web server configuration. To do this, however, the directory must 
first be created.

``` bash
pi@wx:~ $ sudo mkdir /opt/wx/ssl
```

Then copy the pfx file to ```/opt/wx/ssl```

``` bash
pi@wx:~ $ sudo cp wx.pfx /opt/wx/ssl
```

## Installing WebExpress applications
WebExpress has a powerful plugin system. The plugins to be installed and, if applicable, dependencies are copied to the ```/opt/wx/packages``` 
directory (see section Basic configuration). The plugin may need to be configured. For the installation and setup of the plugins, the 
instructions of the plugins are to be used.

# Start WebExpress
For the first start-up or after a change in the configuration, WebExpress must be restarted.

``` bash
pi@wx:~ $ sudo systemctl restart webexpress
```

WebExpress will start automatically after each restart of the Rasperry Pi.

# Installing Certificates in Windows
If https is used with self-generated certificates, the certificates should be stored in the client. The .pfx file must be placed in the 
certificate store under Trusted Root Certification Authorities.

![certificatestore1](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/certificatestore1.png)
![certificatestore2](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/certificatestore2.png)
![certificatestore3](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/certificatestore3.png)
![certificatestore4](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/certificatestore4.png)
![certificatestore5](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/certificatestore5.png)
![certificatestore6](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/certificatestore6.png)
![certificatestore7](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/certificatestore7.png)

The WebExpress certificate must be trusted in the browser.

![trust](https://raw.githubusercontent.com/ReneSchwarzer/WebExpress/doc/assets/trust.png)

# Update
To ensure security, the Raspberry Pi, its applications and WebExpress must be updated regularly.

``` bash
pi@wx:~ $ sudo raspi-config
pi@wx:~ $ sudo apt-get update
pi@wx:~ $ sudo apt-get upgrade
```

The WebExpress binaries are also to be updated. For this purpose, the current binaries from https://github.com/ReneSchwarzer/WebExpress/releases must be used (see section Installing WebExpress).

# Shopping list
The following hardware is required:
- A Raspberry Pi 4 Model B with 8GB
- One plug-in power supply 5V/3A USB Type-C
- A 16GB or 32GB MicroSD card
- Optional housing

# Sources
- [1] https://dotnet.microsoft.com/download/linux-package-manager/debian10/runtime-current
- [2] https://www.ionos.de/digitalguide/server/konfiguration/raspberry-pi-mit-fester-ip-adresse-versehen/#:~:text=Den%20Raspberry%20Pi%20mit%20einer%20festen%20IP-Adresse%20ausstatten.,Zeitraum%20mit%20anderen%20Ger%C3%A4ten%20auf%20ihn%20zugreifen%20will
- [3] https://legacy.thomas-leister.de/eine-eigene-openssl-ca-erstellen-und-zertifikate-ausstellen/
