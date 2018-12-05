Imports System.IO
Module Module1
    ''add maximum string character limit to title(3), forename(15), surname(15) and age(2) and now favorite subject is some limit i need to figure it out 
    ''add nice table format <<< done but clean up code
    ''searching returns more than 1 query e.g searching age "17" returns all valid results
    ''allow to choose what to search by
    ''add removing student from data base (and shift every element after this element down by 1 to close gap)
    ''add password entry

    Dim StudentRecord As New List(Of Student)

    Structure Student
        Dim Title As String
        Dim Forename As String
        Dim Surname As String
        Dim Age As Integer
        Dim Subject As String
    End Structure

    Sub InputRecord()
        Dim Record As Student
        Console.WriteLine("Title: ")
        Record.Title = Console.ReadLine()
        Console.WriteLine("Forename: ")
        Record.Forename = Console.ReadLine()
        Console.WriteLine("Surname: ")
        Record.Surname = Console.ReadLine()
        Console.WriteLine("Age: ")
        Record.Age = Console.ReadLine()
        Console.WriteLine("Favorite Subject: ")
        Record.Subject = Console.ReadLine()
        StudentRecord.Add(Record)
        Console.WriteLine()
        Console.WriteLine($"Added {Record.Forename} {Record.Surname} to the database!")
        Console.WriteLine("Press any key to return to the menu")
        Console.ReadKey()
    End Sub

    Sub LoadFromFile()
        Dim Line As String
        Dim Record As Student
        Using Reader As StreamReader = New StreamReader(Directory.GetCurrentDirectory & "\Database.txt")
            Do Until Reader.EndOfStream()
                Line = Reader.ReadLine()
                Record.Title = Line.Substring(0, 7).Trim()
                Record.Forename = Line.Substring(8, 17).Trim()
                Record.Surname = Line.Substring(26, 17).Trim()
                Record.Age = Int(Line.Substring(43, 5).Trim())
                Record.Subject = Line.Substring(48).Trim()
                StudentRecord.Add(Record)
            Loop
        End Using
    End Sub

    Sub SaveToFile()
        Using Writer As StreamWriter = New StreamWriter(Directory.GetCurrentDirectory & "\Database.txt")
            For i = 0 To StudentRecord.Count - 1
                Writer.WriteLine($"{StudentRecord(i).Title,-7} {StudentRecord(i).Forename,-17} {StudentRecord(i).Surname,-17} {StudentRecord(i).Age,-4} {StudentRecord(i).Subject}")
            Next
        End Using
    End Sub

    Sub OutputRecord(index)
        Console.WriteLine()
        Console.WriteLine(LSet("Title", 8) & LSet("│Forename", 19) & LSet("│Surname", 19) & LSet("│Age", 6) & "│Subject")
        Console.WriteLine($"{StudentRecord(index).Title,-7} │{StudentRecord(index).Forename,-17} │{StudentRecord(index).Surname,-17} │{StudentRecord(index).Age,-4} │{StudentRecord(index).Subject}")
        Console.WriteLine("Press any key to return to the menu")
        Console.ReadKey()
    End Sub

    Sub OutPutAll()
        Dim LongestLength As Integer = 0
        For i = 0 To StudentRecord.Count - 1
            If StudentRecord(i).Subject.Length > LongestLength Then
                LongestLength = StudentRecord(i).Subject.Length
            End If
        Next
        For i = 0 To (56 + LongestLength)
            If i = 8 Or i = 27 Or i = 46 Or i = 52 Then
                COnsole.write("┬")
            ElseIf i = 0 Then
                Console.write("┌")
            ElseIf i = (56 + longestLength) Then
                Console.Write("┐")
            Else
                Console.Write("─")
            End If
        Next
        Console.WriteLine()
        Console.WriteLine(LSet("│Title", 8) & LSet("│Forename", 19) & LSet("│Surname", 19) & LSet("│Age", 6) & LSet("│Subject", 20) & "│")
        For i = 0 To (56 + LongestLength)
            If i = 8 Or i = 27 Or i = 46 Or i = 52 Then
                COnsole.write("┼")
            ElseIf i = 0 Then
                Console.write("├")
            ElseIf i = (56 + longestLength) Then
                Console.Write("┤")
            Else
                Console.Write("─")
            End If
        Next
        Console.WriteLine()
        For i = 0 To StudentRecord.Count - 1
            Console.WriteLine($"│{StudentRecord(i).Title,-6} │{StudentRecord(i).Forename,-17} │{StudentRecord(i).Surname,-17} │{StudentRecord(i).Age,-4} │{StudentRecord(i).Subject,-19}│")
        Next
        For i = 0 To (56 + LongestLength)
            If i = 8 Or i = 27 Or i = 46 Or i = 52 Then
                COnsole.write("┴")
            ElseIf i = 0 Then
                Console.write("└")
            ElseIf i = (56 + longestLength) Then
                Console.Write("┘")
            Else
                Console.Write("─")
            End If
        Next
        Console.WriteLine()
        Console.WriteLine("Press any key to return to the menu")
        Console.ReadKey()
    End Sub
    Sub Main()
        Dim Decision As Integer
        Dim Search As String
        Dim Found As Boolean = False
        While True
            Console.Clear()
            Console.WriteLine("What would you like to do")
            Console.WriteLine("1) Add a record")
            Console.WriteLine("2) Print record by surname")
            Console.WriteLine("3) Print all records")
            Console.WriteLine("4) Exit")
            Console.Write("What would you like to do (1-4): ")
            Decision = Console.ReadLine()
            Threading.Thread.Sleep(150)
            Console.Clear()
            Select Case Decision
                Case 1
                    InputRecord()
                Case 2
                    Console.Write("Which record do you want to print? (By Surname): ")
                    Search = Console.ReadLine()
                    For i = 0 To StudentRecord.Count - 1
                        If StudentRecord(i).Surname.ToLower() = Search.ToLower() Then
                            OutputRecord(i)
                            Found = True
                            Exit For
                        Else
                            Found = False
                        End If
                    Next
                    If Found = False Then
                        Console.WriteLine("Student is not in database")
                        Threading.Thread.Sleep(1200)
                    End If
                Case 3
                    OutPutAll()
                Case 4
                    Exit While
                Case 5
                    SaveToFile()
                Case 6
                    LoadFromFile()
            End Select
        End While
    End Sub

End Module
