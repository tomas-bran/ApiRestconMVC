
namespace MandrilAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(); //Servicio que sirve para implementar patron MVC
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); //Middleware que sirve para cuando creas controllers, automaticamente
                                  //los mapea a una ruta, a una URL de mi API 

            app.Run();
        }
    }
}

//Un servicio es un componente que vamos a usar en nuestra app (algun pedazo de codigo que vayamos a usar).
//Por lo tanto el builder, es un objeto al cual le vamos a agregar todos los servicios que necesitemos para
//buildearlo, y crear nuestra app ----> app = builder.Build()

//Un middleware es un componente que manipula las peticiones HTTP. Son pipelines, es decir se van a ejecutar
//uno despues del otro importando el orden.
//se los utiliza con la palabra Use. Ej: app.UseSwagger()


//MVC es un patron de arquitectura de software. Tiene 3 componentes

/* Model: Lo que transporta e interactua con la data
 * View : Lo que muestra la data (esta parte es mas de Frontend)
 * Controller: Maneja la interaccion entre el usuario, el view y la data (hace TODO)*/