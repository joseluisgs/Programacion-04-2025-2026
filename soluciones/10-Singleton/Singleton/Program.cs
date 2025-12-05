// See https://aka.ms/new-console-template for more information

using Singleton;

var a1 = new NonSingleton();
var a2 = new NonSingleton();
Console.WriteLine(a1);
Console.WriteLine(a2);
var s1 = SingletonA.Instancia();
var s2 = SingletonA.Instancia();
Console.WriteLine(s1);
Console.WriteLine(s2);
var s3 = SingletonB.Instancia;
var s4 = SingletonB.Instancia;
Console.WriteLine(s3);
Console.WriteLine(s4);
