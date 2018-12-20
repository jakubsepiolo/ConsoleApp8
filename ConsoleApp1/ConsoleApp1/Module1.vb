Imports System.IO
Module Module1
    Dim StudentRecord As New List(Of Student) ''Our list used to store records
    Dim AllowedTitles As New List(Of String)(New String() {"Mr", "Mrs", "Ms", "Miss", "Mx", "Master", "Madam"}) ''List of "allowed titles"

    Structure Student ''Declaring our structure used for the records
        Dim Title As String
        Dim Forename As String
        Dim Surname As String
        Dim Age As Integer
        Dim Subject As String
    End Structure

    Sub RemoveRecord(index) ''Removing an element from the list
        For i = index To (StudentRecord.Count - 1)
            If i + 1 <= StudentRecord.Count - 1 Then ''Once removed we need to shift everything down by 1 in order to close the gap between the deleted element
                StudentRecord(i) = StudentRecord(i + 1)
            End If
        Next
        StudentRecord.RemoveAt(StudentRecord.Count - 1)
    End Sub

    Sub SearchDatabase()
        Dim SearchChoice As Integer
        Dim SearchQuery As String
        Console.WriteLine("What do you wish to search by?") ''Ask what they wish to search by
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
                    If StudentRecord(i).Title = SearchQuery Then ''Do a linear search to check if query matches any element in the list
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
        While True
            Console.Write("Title: ")
            Record.Title = Console.ReadLine()
            If Record.Title.Length() < 7 And AllowedTitles.Contains(Record.Title) Then ''Make sure it is less than 7 characters and in the list of allowed ones
                Exit While
            Else
                Console.WriteLine("Input is invalid!")
            End If
        End While
        While True
            Console.Write("Forename: ")
            Record.Forename = Console.ReadLine()
            If Record.Forename.Length() < 17 Then ''Max length 16
                Exit While
            Else
                Console.WriteLine("Input is too long!")
            End If
        End While
        While True
            Console.Write("Surname: ")
            Record.Surname = Console.ReadLine()
            If Record.Surname.Length() < 17 Then ''Max length 16
                Exit While
            Else
                Console.WriteLine("Input is too long!")
            End If
        End While
        While True
            Console.Write("Age: ")
            Record.Age = Console.ReadLine()
            If Record.Age > 0 And Record.Age.ToString.Length < 4 Then ''Max length 4 and greater than 0 integer
                Exit While
            Else
                Console.WriteLine("Input is invalid!")
            End If
        End While
        While True
            Console.Write("Subject: ")
            Record.Subject = Console.ReadLine()
            If Record.Subject.Length() < 19 Then ''Max length 18
                Exit While
            Else
                Console.WriteLine("Input is too long!")
            End If
        End While
        Console.WriteLine($"Added {Record.Forename} {Record.Surname} to the database!") ''Tell the user that their entry has been added to the record
        Console.WriteLine("Press any key to return to the menu")
        Console.ReadKey()
    End Sub

    Sub LoadFromFile()
        Dim Record As Student
        Using Reader As BinaryReader = New BinaryReader(File.Open(Directory.GetCurrentDirectory & "\Database.txt", FileMode.Open))
            For i = 0 To Reader.ReadInt32 - 1 ''Read the first integer in the file which contains the list length so we know how many times to read until the end
                Record.Title = Reader.ReadString.Trim()
                Record.Forename = Reader.ReadString.Trim()
                Record.Surname = Reader.ReadString.Trim()
                Record.Age = Int(Reader.ReadString.Trim())
                Record.Subject = Reader.ReadString.Trim() '' remove whitespace from strings
                StudentRecord.Add(Record)
            Next
        End Using
    End Sub

    Sub SaveToFile()
        Using Writer As BinaryWriter = New BinaryWriter(File.Open(Directory.GetCurrentDirectory & "\Database.txt", FileMode.Create))
            Writer.Write(StudentRecord.Count) ''store the list length at the beginning of the file
            For i = 0 To StudentRecord.Count - 1
                Writer.Write($"{StudentRecord(i).Title,-7}")
                Writer.Write($"{StudentRecord(i).Forename,-17}")
                Writer.Write($"{StudentRecord(i).Surname,-17}")
                Writer.Write($"{StudentRecord(i).Age,-4}")
                Writer.Write($"{StudentRecord(i).Subject}") ''output everything using an individual string for each piece of data
            Next
        End Using
    End Sub

    Sub OutputRecord(index)
        If index <> 0 Then
            DrawMiddle()
            Console.WriteLine()
        End If
        Console.WriteLine($"│{StudentRecord(index).Title,-6} │{StudentRecord(index).Forename,-17} │{StudentRecord(index).Surname,-17} │{StudentRecord(index).Age,-4} │{StudentRecord(index).Subject,-19}│")
    End Sub

    Sub DrawTopTable() ''procedure to draw the top of the table
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
        DrawMiddle()
    End Sub

    Sub DrawMiddle() ''procedure to draw between records
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

    Sub DrawBottomTable() ''procedure to draw bottom of table
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
    Sub OutPutAll() ''prints out all elements of the list
        DrawTopTable()
        Console.WriteLine()
        For i = 0 To StudentRecord.Count - 1
            If i <> 0 Then
                DrawMiddle()
                Console.WriteLine()
            End If
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
        Dim Password As String = "abc123" ''password is set
        While True ''infinite loop until condition inside loop is met
            Console.Write("Password: ")
            Dim key As ConsoleKeyInfo
            Dim PasswordInput As String = ""
            Do
                key = Console.ReadKey(True) ''hide the keys the user inputs
                If (key.Key <> ConsoleKey.Backspace And key.Key <> ConsoleKey.Enter) Then ''if it is not enter or backspace, accept the key as an input to the password
                    PasswordInput += key.KeyChar
                    Console.Write("*") ''write a star where the character would actually appear
                ElseIf key.Key = ConsoleKey.Backspace And Console.CursorLeft > 10 Then ''make sure they can't delete the "Password :" part
                    If key.Key = ConsoleKey.Backspace And PasswordInput.Length > 0 Then
                        PasswordInput = PasswordInput.Substring(0, PasswordInput.Length - 1) ''able to correct their input using backspace
                    End If
                    Console.Write(vbBack)
                    Console.Write(" ")
                    Console.Write(vbBack)
                End If
            Loop Until key.Key = ConsoleKey.Enter ''once enter is pressed the loop ends and the input gets checked
            If PasswordInput = Password Then
                Exit While ''if the input matches to the password then the loop should end
            Else
                Console.WriteLine()
                Console.WriteLine("Invalid password!")

            End If
        End While
        While True ''infinite loop until we get a valid input
            Console.Clear()
            Console.WriteLine("What would you like to do")
            Console.WriteLine("1) Add a record")
            Console.WriteLine("2) Remove a record")
            Console.WriteLine("3) Print record by query")
            Console.WriteLine("4) Print all records")
            Console.WriteLine("5) Save database")
            Console.WriteLine("6) Load database")
            Console.WriteLine("7) Exit")
            Console.Write("What would you like to do (1-7): ")
            Decision = Console.ReadLine()
            Threading.Thread.Sleep(150) ''small delay
            Console.Clear()
            Select Case Decision
                Case 1
                    InputRecord()
                Case 2
                    Console.Write("Which record do you want to remove? (By Surname): ")
                    Search = Console.ReadLine()
                    For i = 0 To StudentRecord.Count - 1
                        If StudentRecord(i).Surname.ToLower() = Search.ToLower() Then '' use case insensetive to avoid issues
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
