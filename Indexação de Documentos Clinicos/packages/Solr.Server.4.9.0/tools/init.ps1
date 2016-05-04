param($installPath, $toolsPath, $package)

Function Initialize()
{
    # The current solution openen in Visual Studio
    $solution = Get-Interface $dte.Solution ([EnvDTE80.Solution2])

    # The path to the directory that contains the current solution
    $path = Split-Path $solution.FullName -Parent

    # Path to the directory to which the Solr configuration files should be copied
    $target = Join-Path $path $package.Id

    $isInstall = -not (Test-Path $target)

    # If $target does not exist we assume installation of the package
    If ($isInstall)
    {
        CopyFiles $target
        AddFilesToSolution $target
    }

    # Add command to the Package Manager Console
    $module = Join-Path $toolsPath "Solr.psm1"
    $server = Join-Path $installPath "server"
    Import-Module $module -ArgumentList @($server, $target) -DisableNameChecking
}

Function CopyFiles($target)
{
    # Path to a directory containing Solr configuration files
    $source = Join-Path $installPath "home"

    # Create the target directory
    New-Item -ItemType Directory -Path $target > $null

    # Copy the Solr configuration files to the target directory
    Copy-Item "$source/*" $target -Recurse
}

Function AddFilesToSolution($target)
{
    # Create the solution folder 'project'
    $project = $solution.AddSolutionFolder($package.Id)

    # Mirror the contents of $target into the solution folder
    AddItemsRecursive $target $project
}

Function AddItemsRecursive($directory, $project)
{
    # Add all files in the directory to the solution folder
    ForEach ($file in Get-ChildItem -File -Path $directory)
    {
        # Get the collection of files in the solution folder
        $items = Get-Interface $project.ProjectItems ([EnvDTE.ProjectItems])

        # Add the file to the solution folder
        $items.AddFromFile($file.FullName) > $null
    }

    # Add all subdirectories to the solution folder recursively
    ForEach ($subdirectory in Get-ChildItem -Directory -Path $directory)
    {
        # Obtain a reference to the solution folder as an actual SolutionFolder interface
        $folder = Get-Interface $project.Object ([EnvDTE80.SolutionFolder])

        # Add a corresponding sub solution folder
        $project = $folder.AddSolutionFolder($subdirectory.Name)

        # Recurse
        AddItemsRecursive $subdirectory.FullName $project
    }
}

Initialize
