# Fortnite Pinger
My First .NET Framework App Build With Visual Studio 2022

Simple Fortnite Pinger to Check Network Stability for Low Stable Network Players

## 🔥 Showcase

## Stable

<img src = "https://github.com/salih101/Fortnite-Pinger/blob/master/Docs/outReady.PNG?raw=true">

## Not Stable

<img src = "https://github.com/salih101/Fortnite-Pinger/blob/master/Docs/outNotReady.PNG?raw=true">

##

Click **FORTNITE** Icon To Launch Game

## ⚙️ Instruction
 **STEP 1**
 
Configure `Fortnite Pinger.exe.config` File

**Example**
```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8"/>
    </startup>
	
		<appSettings>
			<add key="PING_COUNT" value="10"/>
			<add key="TARGET_DOMAIN" value="ping-me.ds.on.epicgames.com" />
			<add key="PING_INTERVAL" value="5" />
			<add key="LATENCY_LIMIT" value="70"/> 
			<add key="FORTNITE_URL" value="com.epicgames.launcher://apps/fn%3A4fe75bbc5a674f4f9b356b5c90567da5%3AFortnite?action=launch&amp;silent=true"/> 
		</appSettings>
	
</configuration>
```
📝 **NOTE :** `&` Character in `FORTNITE URL` Directly Not Supported in XML FileUse `&amp;` Instead





 
 
