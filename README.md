## How to build

For reasons beyond my current understanding, the dependency on Assignment1.dll is broken out of the box when downloading the project as a zip file, so here are my known steps to make it work:

1. In Visual Studio 2022, go to Dependencies -> Assemblies under both projects, and right click remove the Assignment1 dependency.

2. Add a new project dependency for both projects, browse to `\Assignment4\bin\Debug\net6.0` and pick `Assignment1.dll`.
