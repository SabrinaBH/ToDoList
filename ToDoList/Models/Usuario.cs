﻿namespace ToDoList.Models
{
  public class Usuario
  {
    // public Guid Id { get; set;}
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string? PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; }
    public string? Nickname { get; set; }
    public string? Email { get; set; }
    public bool EsUsuarioDeJuego { get; set; }

    public Usuario()
    {
      Id = Guid.NewGuid().ToString();
      //Id = "";
      Nombre = "";
      PrimerApellido = "";
      SegundoApellido = "";
      Email = "";
      EsUsuarioDeJuego = false;
    }

    public Usuario(string nombre, string primerApellido, string segundoApellido, string email, bool esJuego)
    {
      Id = Guid.NewGuid().ToString();
      //Id = id;
      Nombre = nombre;
      PrimerApellido = primerApellido;
      SegundoApellido = segundoApellido;
      Email = email;
      EsUsuarioDeJuego = esJuego;
    }
  }
}