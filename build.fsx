// include Fake lib
#r "packages/build/FAKE/tools/FakeLib.dll"
open Fake
open Fake.FileUtils

RestorePackages()


// version info
let version = "0.0.1"  // or retrieve from CI server

// Properties
let baseDir   = __SOURCE_DIRECTORY__
let buildDir  = "./build/"
let testDir   = "./test/"
let deployDir = "./deploy/"
let installDir = "D:/Portable/Tools/StartTasks/"
let zipPath   = deployDir + "TaskStart." + version + ".zip"


// Targets
Target "Clean" (fun _ ->
  CleanDirs [buildDir; testDir] //; deployDir
)

Target "BuildApp" (fun _ ->
   !! "TaskStart/*.csproj"
   |> MSBuildRelease buildDir "Build"
   |> Log "AppBuild-Output: "
)

//Target "BuildTest" (fun _ ->
//  !! "TaskStart/*.csproj"
//    |> MSBuildDebug testDir "Build"
//    |> Log "TestBuild-Output: "
//)

//Target "Test" (fun _ ->
//  !! (testDir + "/NUnit.Test.*.dll")
//    |> NUnit (fun p ->
//      {p with
//        DisableShadowCopy = true
//        OutputFile = testDir + "TestResults.xml" })
//)

//Target "LibZ" (fun _ ->
//  let libZ = "packages/build/LibZ.Tool/tools/libz.exe"
//  let asmName = "TaskStartFull.exe"
//  let args = sprintf "inject-dll --assembly %s --include *.dll --include TaskStart.exe --move" asmName
//  Shell.Exec(libZ, args, buildDir) |> ignore
//)

Target "Zip" (fun _ ->
  mkdir deployDir
  !! (buildDir + "/**/*.*")
    -- "*.zip"
    |> Zip buildDir (deployDir + "TaskStart." + version + ".zip")
)

Target "Default" (fun _ ->
  trace "Hello World from FAKE"
)

Target "Install" (fun _ ->
  Unzip installDir zipPath
)

// Dependencies
"Clean"
  ==> "BuildApp"
//  ==> "BuildTest"
//  ==> "Test"
//
  ==> "Zip"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
