Module Module1
    Dim Width As Integer = 26
    Dim Height As Integer = 26
    Dim Maze(Height, Width) As Integer
    Dim WasHere(Height, Width) As Boolean
    Dim Finish As Coordinates
    Dim Start As Coordinates
    Dim Direction As Integer
    ''button pressed -> solve from player x,y 
    ''more stuff
    ''comment code

    Structure Coordinates
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Function isValidMove(Player As Coordinates) As Boolean
        Select Case Direction
            Case 87
                If Height - 1 <> -1 Then
                    If Maze(Player.X - 1, Player.Y) <> 1 Then
                        Return True
                    End If
                End If
            Case 65
                If Width - 1 <> -1 Then
                    If Maze(Player.X, Player.Y - 1) <> 1 Then
                        Return True
                    End If
                End If
            Case 83
                If Height + 1 <> -1 Then
                    If Maze(Player.X + 1, Player.Y) <> 1 Then
                        Return True
                    End If
                End If
            Case 68
                If Width + 1 <> -1 Then
                    If Maze(Player.X, Player.Y + 1) <> 1 Then
                        Return True
                    End If
                End If
        End Select
    End Function

    Function generateRandomDirections() As Integer()
        Randomize() ''new seed

        Dim Randoms(3) As Integer ''4 number array
        Dim Temp As Integer

        Temp = Int(1 + (Rnd() * 4))

        For i = 0 To 3

            While Randoms.Contains(Temp) = True ''keep generating new number until 4 unique numbers in array
                Temp = Int(1 + (Rnd() * 4))
            End While

            Randoms(i) = Temp

        Next

        Return Randoms '' return the array of random numbers 

    End Function

    Sub solveMaze(startx As Integer, starty As Integer)
        For Rows = 0 To Height
            For Cols = 0 To Width
                WasHere(Rows, Cols) = False
            Next
        Next
        Dim b As Boolean = recursiveSolve(startx, starty) '' if B is true then there is a solution to the maze
    End Sub

    Function recursiveSolve(x As Integer, y As Integer) As Boolean ''our  solving function
        If x = Finish.X And y = Finish.Y Then '' if the start = end then the path is simply that tile
            Return True
        End If

        If Maze(x, y) = 1 Or WasHere(x, y) Then '' if our current co-ordinates are a wall or we haven't been at this co-ordinate then return false
            Return False
        End If

        WasHere(x, y) = True ''mark this tile as having been here

        If x <> 0 Then ''ensure we don't index out of boundaries
            If recursiveSolve(x - 1, y) Then ''if we get false from going up then we must be on path
                Maze(x, y) = 2 ''add this co-ordinate to the right path
                Return True
            End If
        End If

        If x <> Width Then
            If recursiveSolve(x + 1, y) Then
                Maze(x, y) = 2
                Return True
            End If
        End If

        If y <> 0 Then
            If recursiveSolve(x, y - 1) Then
                Maze(x, y) = 2
                Return True
            End If
        End If

        If y <> Height Then
            If recursiveSolve(x, y + 1) Then
                Maze(x, y) = 2
                Return True
            End If
        End If

    End Function

    Sub populateMaze(x As Integer, y As Integer) ''fill maze with random pattern using depth first searching and recursive calling

        Dim RandomDirs() As Integer = generateRandomDirections() ''receive array of random ints (used for random direction)
        For i = 0 To RandomDirs.Length() - 1

            Select Case RandomDirs(i) ''loop through array of random directions

                Case 1
                    If (x - 2 <= 0) Then
                        Continue For ''if direction is out of bounds then loop to next random direction
                    End If
                    If Maze(x - 2, y) <> 0 Then
                        Maze(x - 2, y) = 0
                        Maze(x - 1, y) = 0
                        populateMaze(x - 2, y)
                    End If
                    Exit Select ''once we find valid path exit the case and move to next direction (otherwise this function will end as it will think we are finished)

                Case 2 ''repeat as above
                    If (y + 2 >= Width) Then
                        Continue For
                    End If
                    If Maze(x, y + 2) <> 0 Then
                        Maze(x, y + 2) = 0
                        Maze(x, y + 1) = 0
                        populateMaze(x, y + 2)
                    End If
                    Exit Select

                Case 3
                    If (x + 2 >= Height) Then
                        Continue For
                    End If
                    If Maze(x + 2, y) <> 0 Then
                        Maze(x + 2, y) = 0
                        Maze(x + 1, y) = 0
                        populateMaze(x + 2, y)
                    End If
                    Exit Select

                Case 4
                    If (y - 2 <= 0) Then
                        Continue For
                    End If
                    If Maze(x, y - 2) <> 0 Then
                        Maze(x, y - 2) = 0
                        Maze(x, y - 1) = 0
                        populateMaze(x, y - 2)
                    End If
                    Exit Select

            End Select

        Next

    End Sub

    Sub Main()
        Randomize() ''seed
        Dim Player As Coordinates
        For Rows = 0 To Height
            For Cols = 0 To Width
                Maze(Rows, Cols) = 1 '' fill maze with walls
            Next
        Next

        Dim R As Integer, C As Integer

#Region "Starting co-ordinates"
        R = Int(Rnd() * Height)
        While R Mod 2 = 0
            R = Int(Rnd() * Height)
        End While
        C = Int(Rnd() * Width) ''finding random starting position with odd cords
        While C Mod 2 = 0
            C = Int(Rnd() * Width)
        End While
#End Region
        Maze(R, C) = 0 ''make our starting cord a path
        populateMaze(R, C) ''begin carving out a path

#Region "Create Start And End"
        Dim Tempx As Integer = Int(Rnd() * Height)
        Tempx = Tempx
        If Tempx - 1 <= 0 Or Tempx + 1 >= Height Then
            Tempx = Int(Rnd() * Height)
        End If
        While Maze(Tempx, 1) = 1 ''logic to find opening in wall that allows you to move from start
            Tempx = Int(Rnd() * Height)
            If Tempx - 1 <= 0 Or Tempx + 1 >= Height Then
                Tempx = Int(Rnd() * Height)
            End If
        End While
        Maze(Tempx, 0) = 0
        Start.X = Tempx
        Start.Y = 0
        While Maze(Tempx, Width - 1) = 1 ''logic to find ending in wall that allows you to move to end
            Tempx = Int(Rnd() * Width)
            If Tempx - 1 <= 0 Or Tempx + 1 >= Width Then
                Tempx = Int(Rnd() * Width)
            End If
        End While
        Maze(Tempx, Width) = 0
        Finish.X = Tempx
        Finish.Y = Width
#End Region
        Player.X = Start.X
        Player.Y = Start.Y
        Maze(Player.X, Player.Y) = 3
        Maze(Finish.X, Finish.Y) = 2
        solveMaze(Start.X, Start.Y) '' solve maze using recursive solving (find path)
        For Rows = 0 To Height

            For Cols = 0 To Width

                If Maze(Rows, Cols) = 1 Then
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.White
                    Console.Write(" " & Maze(Rows, Cols))

                ElseIf Maze(Rows, Cols) = 2 Then

                    Console.ForegroundColor = ConsoleColor.Cyan ''print solution path in cyan
                    Console.BackgroundColor = ConsoleColor.Cyan
                    Console.Write(" " & Maze(Rows, Cols))

                ElseIf Maze(Rows, Cols) = 3 Then

                    Console.ForegroundColor = ConsoleColor.Red ''print solution path in cyan
                    Console.BackgroundColor = ConsoleColor.Red
                    Console.Write(" " & Maze(Rows, Cols))

                Else

                    Console.ForegroundColor = ConsoleColor.Black
                    Console.BackgroundColor = ConsoleColor.Black
                    Console.Write(" " & Maze(Rows, Cols))

                End If
            Next

            Console.WriteLine()

        Next

        While Player.X <> Finish.X Or Player.Y <> Finish.Y
            Direction = Console.ReadKey(True).Key
            Console.CursorVisible = False
            Select Case Direction
                Case 87
                    If isValidMove(Player) = True Then
                        Maze(Player.X, Player.Y) = 0
                        Player.X -= 1
                        Maze(Player.X, Player.Y) = 3
                        For i = 0 To Width
                            Console.SetCursorPosition(Player.Y * 2, i)
                            If Maze(i, Player.Y) = 1 Then
                                Console.ForegroundColor = ConsoleColor.White
                                Console.BackgroundColor = ConsoleColor.White
                            ElseIf Maze(i, Player.Y) = 2 Then
                                Console.BackgroundColor = ConsoleColor.Cyan
                                Console.ForegroundColor = ConsoleColor.Cyan
                            ElseIf Maze(i, Player.Y) = 3 Then
                                Console.BackgroundColor = ConsoleColor.Red
                                Console.ForegroundColor = ConsoleColor.Red
                            Else
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Black
                            End If
                            Console.Write("  ")
                        Next
                    End If
                Case 65
                    If isValidMove(Player) = True Then
                        Maze(Player.X, Player.Y) = 0
                        Player.Y -= 1
                        Maze(Player.X, Player.Y) = 3
                        For i = 0 To Height
                            Console.SetCursorPosition(i * 2, Player.X)
                            If Maze(Player.X, i) = 1 Then
                                Console.ForegroundColor = ConsoleColor.White
                                Console.BackgroundColor = ConsoleColor.White
                            ElseIf Maze(Player.X, i) = 2 Then
                                Console.BackgroundColor = ConsoleColor.Cyan
                                Console.ForegroundColor = ConsoleColor.Cyan
                            ElseIf Maze(Player.X, i) = 3 Then
                                Console.BackgroundColor = ConsoleColor.Red
                                Console.ForegroundColor = ConsoleColor.Red
                            Else
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Black
                            End If
                            Console.Write("  ")
                        Next

                    End If
                Case 83
                    If isValidMove(Player) = True Then
                        Maze(Player.X, Player.Y) = 0
                        Player.X += 1
                        Maze(Player.X, Player.Y) = 3
                        For i = 0 To Width
                            Console.SetCursorPosition(Player.Y * 2, i)
                            If Maze(i, Player.Y) = 1 Then
                                Console.ForegroundColor = ConsoleColor.White
                                Console.BackgroundColor = ConsoleColor.White
                            ElseIf Maze(i, Player.Y) = 2 Then
                                Console.BackgroundColor = ConsoleColor.Cyan
                                Console.ForegroundColor = ConsoleColor.Cyan
                            ElseIf Maze(i, Player.Y) = 3 Then
                                Console.BackgroundColor = ConsoleColor.Red
                                Console.ForegroundColor = ConsoleColor.Red
                            Else
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Black
                            End If
                            Console.Write("  ")
                        Next
                    End If
                Case 68
                    If isValidMove(Player) = True Then
                        Maze(Player.X, Player.Y) = 0
                        Player.Y += 1
                        Maze(Player.X, Player.Y) = 3
                    End If
                    For i = 0 To Height
                        Console.SetCursorPosition(i * 2, Player.X)
                        If Maze(Player.X, i) = 1 Then
                            Console.ForegroundColor = ConsoleColor.White
                            Console.BackgroundColor = ConsoleColor.White
                        ElseIf Maze(Player.X, i) = 2 Then
                            Console.BackgroundColor = ConsoleColor.Cyan
                            Console.ForegroundColor = ConsoleColor.Cyan
                        ElseIf Maze(Player.X, i) = 3 Then
                            Console.BackgroundColor = ConsoleColor.Red
                            Console.ForegroundColor = ConsoleColor.Red
                        Else
                            Console.ForegroundColor = ConsoleColor.Black
                            Console.BackgroundColor = ConsoleColor.Black
                        End If
                        Console.Write("  ")
                    Next
            End Select
        End While
        Console.ResetColor()
        Console.Clear()
        Main()

    End Sub


End Module
