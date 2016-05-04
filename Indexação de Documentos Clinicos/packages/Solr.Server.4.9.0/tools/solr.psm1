param($server, $home)

$java = "java.exe"
$port = "8983"
$context = "/solr"

<#
.SYNOPSIS
    Starts the Solr server
.DESCRIPTION
    This command starts the Solr server at http://localhost:8983 and with 
    solr.home set to the Solr.Server directory. Any cores located beneath 
    this directory will automatically be loaded.
.EXAMPLE
    Start-Solr
#>
Function Start-Solr
{
    # Remember current working directory (cwd)
    Push-Location

    # Change cwd for Jetty which will use it as its temporary directory
    Set-Location $server

    # Source overridable properties from a configuration file
    . (Join-Path $home "config.ps1")

    # Path to the Jetty start command
    $jar = Join-Path $server "start.jar"

    # Create the required arguments for the start command
    $jargs = @(
        "-Djetty.home=`"$server`"", 
        "-Dsolr.solr.home=`"$home`"", 
        "-Djetty.port=$port", 
        "-DhostContext=$context", 
        "-jar `"$jar`""
    )

    # Run the Java process in a separate command window
    Start-Process $java -ArgumentList $jargs

    # Reset the cwd
    Pop-Location
}

<#
.SYNOPSIS
    Opens the Solr administrative web application
.DESCRIPTION
    This command only prints the URL of the Solr administrative web 
    application to the console. You must copy it to your browser's address 
    bar to access the web application.
.EXAMPLE
    Open-SolrAdmin
#>
Function Open-SolrAdmin
{
    # Source overridable properties from a configuration file
    . (Join-Path $home "config.ps1")

    # Buidl URL to the Solr administration web applicaion
    $url = "http://localhost$(If ($port -ne 80) { ':' + $port })$context"

    # Open $url in the default web browser
    [System.Diagnostics.Process]::Start($url)
}

# Export all commands to the Package Manager Console
Export-ModuleMember Start-Solr
Export-ModuleMember Open-SolrAdmin