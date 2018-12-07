Imports System.IO
Module Module1
    ''add maximum string character limit to title(3), forename(15), surname(15) and age(2) and now favorite subject is some limit i need to figure it out << least priority
    ''add nice table format <<< done but clean up code (and fix empty table issue from search query)
    ''searching returns more than 1 query e.g searching age "17" returns all valid results << done, tidy up
    ''allow to choose what to search by << started (todo: implement this into removing) maybe return index value of matched searches
    ''add removing student from data base (and shift every element after this element down by 1 to close gap) << done, need tidy up and tie into searching
    ''add password entry << done, need tidy up
    ''TIDY UP ALL CODE
    Dim StudentRecord As New List(Of Student)

    Structure Student
        Dim Title As String
        Dim Forename As String
        Dim Surname As String
        Dim Age As Integer
        Dim Subject As String
    End Structure

    Sub RemoveRecord(index)
        For i = index To (StudentRecord.Count - 1)
            If i + 1 <= StudentRecord.Count - 1 Then
                StudentRecord(i) = StudentRecord(i + 1)
            End If
        Next
        StudentRecord.RemoveAt(StudentRecord.Count - 1)
    End Sub

    Sub SearchDatabase()
        Dim SearchChoice As Integer
        Dim SearchQuery As String
        Console.WriteLine("What do you wish to search by?")
        Console.WriteLine("1) Title")
        Console.WriteLine("2) Forename")
        Console.WriteLine("3) Surname")
        Console.WriteLine("4) Age")
        Console.WriteLine("5) Subject")
        SearchChoice = Console.ReadLine()
        Select Case SearchChoice
            Case 1
                Console.WriteLine("Which title do you wish to search by: ")
                SearchQuery = Console.ReadLine()
                DrawTopTable()
                Console.WriteLine()
                For i = 0 To StudentRecord.Count - 1
                    If StudentRecord(i).Title = SearchQuery Then
                        OutputRecord(i)
                    End If
                Next
                DrawBottomTable()
            Case 2
                Console.WriteLine("Which forename do you wish to search by: ")
                SearchQuery = Console.ReadLine()
                DrawTopTable()
                Console.WriteLine()
                For i = 0 To StudentRecord.Count - 1
                    If StudentRecord(i).Forename.Contains(SearchQuery) = True Then
                        OutputRecord(i)
                    End If
                Next
                DrawBottomTable()
            Case 3
                Console.WriteLine("Which surname do you wish to search by: ")
                SearchQuery = Console.ReadLine()
                DrawTopTable()
                Console.WriteLine()
                For i = 0 To StudentRecord.Count - 1
                    If StudentRecord(i).Surname.Contains(SearchQuery) = True Then
                        OutputRecord(i)
                    End If
                Next
                DrawBottomTable()
            Case 4
                Console.WriteLine("What age do you wish to search by: ")
                SearchQuery = Int(Console.ReadLine())
                DrawTopTable()
                Console.WriteLine()
                For i = 0 To StudentRecord.Count - 1
                    If StudentRecord(i).Age = SearchQuery Then
                        OutputRecord(i)
                    End If
                Next
                DrawBottomTable()
            Case 5
                Console.WriteLine("What subject do you wish to search by: ")
                SearchQuery = Console.ReadLine()
                DrawTopTable()
                Console.WriteLine()
                For i = 0 To StudentRecord.Count - 1
                    If StudentRecord(i).Subject.Contains(SearchQuery) = True Then
                        OutputRecord(i)
                    End If
                Next
                DrawBottomTable()
        End Select
    End Sub

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
        Console.WriteLine($"│{StudentRecord(index).Title,-6} │{StudentRecord(index).Forename,-17} │{StudentRecord(index).Surname,-17} │{StudentRecord(index).Age,-4} │{StudentRecord(index).Subject,-19}│")
    End Sub

    Sub DrawTopTable()
        For i = 0 To 72
            If i = 8 Or i = 27 Or i = 46 Or i = 52 Then
                Console.Write("┬")
            ElseIf i = 0 Then
                Console.Write("┌")
            ElseIf i = 72 Then
                Console.Write("┐")
            Else
                Console.Write("─")
            End If
        Next
        Console.WriteLine()
        Console.WriteLine(LSet("│Title", 8) & LSet("│Forename", 19) & LSet("│Surname", 19) & LSet("│Age", 6) & LSet("│Subject", 20) & "│")
        For i = 0 To 72
            If i = 8 Or i = 27 Or i = 46 Or i = 52 Then
                Console.Write("┼")
            ElseIf i = 0 Then
                Console.Write("├")
            ElseIf i = 72 Then
                Console.Write("┤")
            Else
                Console.Write("─")
            End If
        Next
    End Sub

    Sub DrawBottomTable()
        For i = 0 To 72
            If i = 8 Or i = 27 Or i = 46 Or i = 52 Then
                Console.Write("┴")
            ElseIf i = 0 Then
                Console.Write("└")
            ElseIf i = 72 Then
                Console.Write("┘")
            Else
                Console.Write("─")
            End If
        Next
    End Sub
    Sub OutPutAll()
        DrawTopTable()
        Console.WriteLine()
        For i = 0 To StudentRecord.Count - 1
            Console.WriteLine($"│{StudentRecord(i).Title,-6} │{StudentRecord(i).Forename,-17} │{StudentRecord(i).Surname,-17} │{StudentRecord(i).Age,-4} │{StudentRecord(i).Subject,-19}│")
        Next
        DrawBottomTable()
        Console.WriteLine()
        Console.WriteLine("Press any key to return to the menu")
        Console.ReadKey()
    End Sub
    Sub Main()
        Dim Decision As Integer
        Dim Search As String
        Dim Found As Boolean = False
        Dim Password As String = "abc123"
        While True
            Console.Write("Password: ")
            Dim key As ConsoleKeyInfo
            Dim PasswordInput As String = ""
            Do
                key = Console.ReadKey(True)
                If (key.Key <> ConsoleKey.Backspace And key.Key <> ConsoleKey.Enter) Then
                    PasswordInput += key.KeyChar
                    Console.Write("*")
                ElseIf key.Key = ConsoleKey.Backspace Then
                    If key.Key = ConsoleKey.Backspace And PasswordInput.Length > 0 Then
                        PasswordInput = PasswordInput.Substring(0, PasswordInput.Length - 1)
                    End If
                    Console.Write(vbBack)
                        Console.Write(" ")
                        Console.Write(vbBack)
                    End If
            Loop Until key.Key = ConsoleKey.Enter
            If PasswordInput = Password Then
                Exit While
            Else
                Console.WriteLine()
                Console.WriteLine("Invalid password!")

            End If
        End While
        While True
            Console.Clear()
            Console.WriteLine("What would you like to do")
            Console.WriteLine("1) Add a record")
            Console.WriteLine("2) Remove a record")
            Console.WriteLine("3) Print record by query")
            Console.WriteLine("4) Print all records")
            Console.WriteLine("5) Save database")
            Console.WriteLine("6) Load database")
            Console.WriteLine("7) Exit")
            Console.Write("What would you like to do (1-6): ")
            Decision = Console.ReadLine()
            Threading.Thread.Sleep(150)
            Console.Clear()
            Select Case Decision
                Case 1
                    InputRecord()
                Case 2
                    Console.Write("Which record do you want to remove? (By Surname): ")
                    Search = Console.ReadLine()
                    For i = 0 To StudentRecord.Count - 1
                        If StudentRecord(i).Surname.ToLower() = Search.ToLower() Then
                            RemoveRecord(i)
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
                    SearchDatabase()
                    Console.WriteLine()
                    Console.Write("Press any key to return to the menu")
                    Console.ReadKey()
                Case 4
                    OutPutAll()
                Case 5
                    SaveToFile()
                Case 6
                    LoadFromFile()
                Case 7
                    Exit While
            End Select
        End While
    End Sub

End Module
