Module Module1

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
    End Sub

    Sub OutputRecord(index)
        Console.WriteLine()
        Console.WriteLine($"{StudentRecord(index).Title} {StudentRecord(index).Forename} {StudentRecord(index).Surname} {StudentRecord(index).Age} {StudentRecord(index).Subject}")
    End Sub

    Sub OutPutAll()
        For i = 0 To StudentRecord.Count - 1
            Console.WriteLine($"{StudentRecord(i).Title} {StudentRecord(i).Forename} {StudentRecord(i).Surname} {StudentRecord(i).Age} {StudentRecord(i).Subject}")
        Next
    End Sub
    Sub Main()
        InputRecord()
        InputRecord()
        InputRecord()
        Console.WriteLine()
        OutputRecord(0)
        Console.WriteLine()
        OutPutAll()
        Console.ReadKey()
    End Sub

End Module
