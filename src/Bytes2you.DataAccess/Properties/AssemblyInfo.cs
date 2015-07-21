using System.Reflection;

[assembly: AssemblyTitle("Bytes2you.DataAccess")]
[assembly: AssemblyCompany("bytes2you")]
[assembly: AssemblyProduct("Bytes2you.DataAccess")]
[assembly: AssemblyDescription("Core abstraction for data access leveraging the Repository design pattern and other related.")]
[assembly: AssemblyCopyright("Copyright bytes2you©  2015")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

#if RELEASEOFFICIAL
[assembly: AssemblyConfiguration("ReleaseOfficial")]
#elif DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif