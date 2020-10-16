using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Pong
{
    class Program : GameWindow
    {
        int Ball_X = 0;
        int Ball_Y = 0;
        int Ball_Size = 20;
        int Speed_BallX = 3;
        int Speed_BallY = 3;
        int Y_Jogador1 = 0;
        int Y_Jogador2 = 0;


        int XJogador1()
        {
            return -ClientSize.Width / 2 + SizeJogador() / 2;
        }

        int XJogador2()
        {
            return ClientSize.Width / 2 - SizeJogador() / 2;
        }

        int SizeJogador()
        {
            return Ball_Size;
        }

        int HeightJogador()
        {
            return 3 * Ball_Size;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            //Move Ball

            Ball_X += Speed_BallX;
            Ball_Y += Speed_BallY;

            // Move in axis X

            if (Ball_X + Ball_Size / 2 > XJogador2() - SizeJogador() / 2 && Ball_Y - Ball_Size / 2 < Y_Jogador2 + HeightJogador() / 2 && Ball_Y +  Ball_Size / 2 > Y_Jogador2 - HeightJogador() / 2)
            {
                Speed_BallX = -Speed_BallX;
            }

           if(Ball_X - Ball_Size / 2 < XJogador1() + SizeJogador() / 2 && Ball_Y - Ball_Size / 2 < Y_Jogador1 + HeightJogador() / 2 && Ball_Y + Ball_Size / 2 > Y_Jogador1 - HeightJogador() / 2)
            {
                Speed_BallX = -Speed_BallX;
            }

          if(Ball_X > ClientSize.Width / 2 || Ball_X < -ClientSize.Width / 2)
            {
                Ball_X = 0;
                Ball_Y = 0;
            }

            //Move in axis Y

            if(Ball_Y + Ball_Size /2 > ClientSize.Height/2)
            {
                Speed_BallY = -Speed_BallY;
            }

            if(Ball_Y - Ball_Size-2 < -ClientSize.Height/2)
            {
                Speed_BallY = -Speed_BallY;
            }

            //Action Button Up

            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                Y_Jogador1 += 5; 
            }

            if(Keyboard.GetState().IsKeyDown(Key.Up))
            {
                Y_Jogador2 += 5;
            }


            //Action Button Down

            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                Y_Jogador1 -= 5;
            }

            if(Keyboard.GetState().IsKeyDown(Key.Down))
            {
                Y_Jogador2 -= 5;
            }
            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            Console.WriteLine(ClientSize.Width +"  "+ ClientSize.Height);

            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

           
                    DesenharRetangulo(Ball_X, Ball_Y, 10, 10, 1.0f, 1.0f, 0.0f ); //pixel bola
                    DesenharRetangulo(XJogador1(), Y_Jogador1, SizeJogador(), HeightJogador(), 1.0f, 0.0f, 0.0f); //pixel jogador 1
                    DesenharRetangulo(XJogador2(), Y_Jogador2, SizeJogador(), HeightJogador(), 0.0f, 0.0f, 1.0f); //pixel jogador 2




            SwapBuffers();


        }

        void DesenharRetangulo(int x, int y, int width, int height, float r, float g, float b)
        {

            GL.Color3(r, g, b);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2( 0.5f * width + x, 0.5f * height + y);
            GL.Vertex2( 0.5f * width + x, -0.5f * height + y);
            GL.Vertex2(-0.5f * width + x, -0.5f * height + y);
            GL.Vertex2(-0.5f * width + x, 0.5f * height + y );

            GL.End();
            
            //Console.WriteLine(height);
        }

        void Movimentar()
        {

        }
        static void Main(string[] args)
        {
            new Program().Run();

        }
    }
}
