Imports System.IO
Module Module1

    Sub Main()
        Dim fileName As String = "H:\MyFile.txt"
        Dim line As String
        Using Reader As StreamReader = New StreamReader(fileName)
            Do Until Reader.EndOfStream
                line = Reader.ReadLine()
                If line.Substring(1).ToLower() = "red" Then
                    Console.ForegroundColor = ConsoleColor.Red
                ElseIf line.Substring(1).ToLower() = "darkgreen" Then
                    Console.ForegroundColor = ConsoleColor.DarkGreen
                ElseIf line.Substring(1).ToLower() = "gray" Then
                    Console.ForegroundColor = ConsoleColor.Gray
                ElseIf line.Substring(1).ToLower() = "white" Then
                    Console.ForegroundColor = ConsoleColor.White
                ElseIf line.Substring(1).ToLower() = "green" Then
                    Console.ForegroundColor = ConsoleColor.Green
                ElseIf line.Substring(1).ToLower() = "blue" Then
                    Console.ForegroundColor = ConsoleColor.Blue
                ElseIf line.Substring(1).ToLower() = "stop" Then
                    Exit Do
                Else
                    Console.WriteLine(line)
                End If

            Loop
        End Using
        Console.ReadKey()
    End Sub
End Module
