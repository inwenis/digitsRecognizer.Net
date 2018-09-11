This is my C# version of https://github.com/c4fsharp/Dojo-Digits-Recognizer and an exercise into profiling and optimization.
Here are the execution times for differene versions: (all available as git commits)
    skip Math.Sqrt:                             00:00:01.8183726 (it's not necessary sice we can compare squared distances)
    replace Math.Pow with plain multiplication: 00:00:02.1072226
    add parallelization:                        00:00:16.0794134
    initial version:                            00:01:04.3763222
