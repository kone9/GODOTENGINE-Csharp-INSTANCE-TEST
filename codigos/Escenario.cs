using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public class Escenario : Spatial
{
    [Export]
    PackedScene cubos;
    //public readonly PackedScene cubos = (PackedScene)ResourceLoader.Load("res://escenas/cubo.tscn");
    Label cantidadDeCubos;
    Label fps;
    Position3D posicionInicial;
    RigidBody instanciarCubo;
    Godot.Timer TimerHacerStatic;
    Queue<RigidBody> cubosGuardados = new Queue<RigidBody>();
    RigidBody cambiarForma;
   
    
    
    float tiempo;
  
    int contador = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cantidadDeCubos = GetNode<Label>("Control/CenterContainer2/cantidad_de_cubos");
        posicionInicial = GetNode<Position3D>("posicion_inicial_cubos");
        TimerHacerStatic = (Godot.Timer)GetTree().GetNodesInGroup("TimerHacerStatic")[0];
        fps = GetNode<Label>("Control/VBoxContainer/FPS");
        
    }
            

    public override void _PhysicsProcess(float delta)
    {

        tiempo = Engine.GetFramesPerSecond();
       
        fps.Text = "FPS " + tiempo.ToString();
        
    }

    private void _on_Timer_timeout()
    {
        contador ++;
        cantidadDeCubos.Text = "cantidad de cubos = " + contador.ToString(); 
        instanciarCubo = (RigidBody)cubos.Instance();
        AddChild(instanciarCubo);
        instanciarCubo.Translation = posicionInicial.Translation;
        cubosGuardados.Enqueue(instanciarCubo);
        //TimerHacerStatic.Start();
        //GD.Print(cubosGuardados);
    }

    private void _on_TimerHacerStatic_timeout()
    {
        //GD.Print("comienza el timer hacer static");
        cambiarForma = cubosGuardados.Dequeue();
        cambiarForma.Sleeping = true;
        //GD.Print(cambiarForma.Name);
        //cambiarForma.Mode = Godot.RigidBody.ModeEnum.Static;
    }

   
}
        
        
