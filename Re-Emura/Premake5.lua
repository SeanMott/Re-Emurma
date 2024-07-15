workspace "ReEmura"
architecture "x64"
startproject "ReEmuraRuntime"

configurations {
    "Debug",
    "Release",
    "Dist"
}

--runtime/main runner
project "ReEmuraRuntime"
kind "ConsoleApp"
location "ReEmura"
language "C++"

targetdir ("bin/%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}-%{cfg.startproject}/%{prj.name}")
objdir ("bin-obj/%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}-%{cfg.startproject}/%{prj.name}")


files 
{
    ---base code
    "ReEmura/includes/**.h",
    "ReEmura/src/**.c",
    "ReEmura/includes/**.hpp",
    "ReEmura/src/**.cpp"
}

includedirs
{
    "ReEmura/includes",
    "ReEmura_ERBParser/includes",

    "Venders/fmt/include"
}

links
{
    "ReEmura_ERBParser"
}

defines
{

}

flags
{
    "NoRuntimeChecks",
    "MultiProcessorCompile"
}

buildoptions { "/utf-8" } --used for fmt

--platforms
filter "system:windows"
    cppdialect "C++20"
    staticruntime "On"
    systemversion "latest"

    defines
    {
        "Window_Build",
        "Desktop_Build"
    }

--configs
filter "configurations:Debug"
    --defines "BTD_DEBUG"
    symbols "On"

    links
    {
        --"Venders/VenderBuilds/SDL/RelWithDebInfo/SDL3.lib"
    }

filter "configurations:Release"
    --defines "BTD_RELEASE"
    optimize "On"

    flags
    {
        
    }

    links
    {
       --"Venders/VenderBuilds/SDL/Release/SDL3.lib"
    }

filter "configurations:Dist"
    --defines "BTD_DIST"
    optimize "On"

    defines
    {
        "NDEBUG"
    }

    flags
    {
       "LinkTimeOptimization"
    }

    links
    {
       --"Venders/VenderBuilds/SDL/MinSizeRel/SDL3.lib"
    }

--parser for the ERB file format
project "ReEmura_ERBParser"
kind "StaticLib"
location "ReEmura_ERBParser"
language "C++"

targetdir ("bin/%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}-%{cfg.startproject}/%{prj.name}")
objdir ("bin-obj/%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}-%{cfg.startproject}/%{prj.name}")


files 
{
    ---base code
    "ReEmura_ERBParser/includes/**.h",
    "ReEmura_ERBParser/src/**.c",
    "ReEmura_ERBParser/includes/**.hpp",
    "ReEmura_ERBParser/src/**.cpp"
}

includedirs
{
    "ReEmura_ERBParser/includes",

    "Venders/fmt/include"
}

links
{
    
}

defines
{

}

flags
{
    "NoRuntimeChecks",
    "MultiProcessorCompile"
}

buildoptions { "/utf-8" } --used for fmt

--platforms
filter "system:windows"
    cppdialect "C++20"
    staticruntime "On"
    systemversion "latest"

    defines
    {
        "Window_Build",
        "Desktop_Build"
    }

--configs
filter "configurations:Debug"
    --defines "BTD_DEBUG"
    symbols "On"

    links
    {
        --"Venders/VenderBuilds/SDL/RelWithDebInfo/SDL3.lib"
    }

filter "configurations:Release"
    --defines "BTD_RELEASE"
    optimize "On"

    flags
    {
        
    }

    links
    {
       --"Venders/VenderBuilds/SDL/Release/SDL3.lib"
    }

filter "configurations:Dist"
    --defines "BTD_DIST"
    optimize "On"

    defines
    {
        "NDEBUG"
    }

    flags
    {
       "LinkTimeOptimization"
    }

    links
    {
       --"Venders/VenderBuilds/SDL/MinSizeRel/SDL3.lib"
    }