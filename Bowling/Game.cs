using System;


namespace Bowling
{
    public class Game
    {
        Score[] scoreArray = new Score[10];

        public int pins;
        public int frame = 0;
        public int lastFrame = 2;
        public int totalScore = 0;

        public Game()
        {
            for (int i = 0; i < 10; ++i)
            {
                scoreArray[i] = new Score();
            }
        }

        public void Roll(int pins)
        {
            this.pins = pins;

            if (this.frame != this.lastFrame)
            {
                scoreArray[frame].firstThrow = pins;
                scoreArray[frame].firstThrowDone = true;
                scoreArray[frame].ValidateThrow();
            }
            else if (this.frame == 9 && scoreArray[frame].secondThrowDone)
            {
                scoreArray[frame].thirdThrow = pins;
                scoreArray[frame].thirdThrowDone = true;
                scoreArray[frame].ValidateThrow();
            }
            else// if(this.frame == this.lastFrame)
            {
                scoreArray[frame].secondThrow = pins;
                scoreArray[frame].secondThrowDone = true;
                scoreArray[frame].ValidateThrow();
            }


            if (frame > 0)
            {
                /* 
                 * Spare bonus for the previous frame
                 */
                if (scoreArray[frame - 1].spare && !scoreArray[frame].secondThrowDone)
                {
                    scoreArray[frame - 1].totalFrameScore += scoreArray[frame].firstThrow;
                }

                /* Strike bonus for the previous frame,
                 * but not on the third roll of the last frame.
                 */
                else if (scoreArray[frame - 1].strike && scoreArray[frame].secondThrowDone && !scoreArray[frame].thirdThrowDone)
                {
                    scoreArray[frame - 1].totalFrameScore += scoreArray[frame].firstThrow + scoreArray[frame].secondThrow;
                }
            }

            Console.WriteLine("Frame: " + this.frame);
            this.lastFrame = this.frame;
        }

        public int Score()
        {
            for (int i = 0; i < 10; ++i)
            {
                this.totalScore += scoreArray[i].totalFrameScore;
            }

            return this.totalScore;
        }

        public void GameLoop()
        {
            int n1, n2, n3 = 0;

            while (frame < 10)
            {
                Console.WriteLine("Enter number of knocked pins: ");
                if (int.TryParse(Console.ReadLine(), out n1))
                {
                    Console.WriteLine("Rolling first");
                    this.Roll(n1);

                    //if not strike, roll again
                    if ((n1 < 10) || this.frame == 9)
                    {
                        Console.WriteLine("Enter number of knocked pins: ");
                        if (int.TryParse(Console.ReadLine(), out n2))
                        {
                            Console.WriteLine("Rolling second");
                            this.Roll(n2);

                            //last frame second throw could eigher be a spare or strike
                            if (this.frame == 9 && scoreArray[frame].secondThrowDone && (scoreArray[frame].spare || scoreArray[frame].strike))
                            {
                                Console.WriteLine("Enter number of knocked pins: ");
                                int.TryParse(Console.ReadLine(), out n3);
                                this.Roll(n3);
                            }

                            this.frame++;

                        }
                        else
                            break;
                    }
                    else
                        this.frame++;
                }
                else
                    break;
            }//while

            Console.WriteLine("Total score: " + this.Score());
            Console.ReadLine();
        }
    }

    public class Score
    {
        public int firstThrow, secondThrow, thirdThrow = 0;
        public int totalFrameScore = 0;
        public bool spare, strike, firstThrowDone, secondThrowDone, thirdThrowDone = false;

        public void ValidateThrow()
        {
            this.totalFrameScore = this.firstThrow + this.secondThrow + this.thirdThrow;

            if (this.totalFrameScore == 10 && this.secondThrow > 0)
            {
                this.spare = true;
            }
            else if (this.totalFrameScore == 10)
                this.strike = true;
        }
    }
}