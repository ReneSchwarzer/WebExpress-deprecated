#!/bin/bash
# Erstellt und verteilt den WebExpress-Service, ohne ihn zu starten.
# Das Starten und Stoppen erfolgt automatisch (Starten beim booten) 
# oder manuell über die Skripte start.sh und stop.sh.

export PATH=$PATH:/usr/share/dotnet-sdk/
export DOTNET_ROOT=/usr/share/dotnet-sdk/ 

dotnet build
dotnet publish

if (sudo systemctl -q is-enabled webexpress.service)
then
    sudo systemctl disable webexpress.service 
fi

sudo mkdir -p /opt/wx
sudo chmod +x /opt/wx


cp -Rf WebExpress.App/bin/Debug/net5.0/publish/* /opt/wx
cp webexpress.sh /opt/wx
sudo chmod +x /opt/wx/webexpress.sh

sudo cp webexpress.service /etc/systemd/system
sudo systemctl enable webexpress.service
