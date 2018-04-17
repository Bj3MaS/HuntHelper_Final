using HuntHelper.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace HuntHelper.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.CreateDatabaseIfNotExists{HuntHelper.DataAccess.HuntHelperContext}" />
    public class HuntHelperDBInitializer : CreateDatabaseIfNotExists<HuntHelperContext>
    {
        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding.
        /// </summary>
        /// <param name="context">The context to seed.</param>
        protected override void Seed(HuntHelperContext context)
        {
            
            context.Animals.Add(new Animal("Bever", "01.10", "30.04", "Ingen spesielle bestemmelser", "Bever.jpg", false));
            context.Animals.Add(new Animal("Beverrotte", "21.08", "15.04", "Ingen spesielle bestemmelser", "Beverrotte.jpg", false));
            context.Animals.Add(new Animal("Bisamrotte", "01.04", "31.03", "Ingen spesielle bestemmelser", "Bisamrotte.jpg", false));
            context.Animals.Add(new Animal("Brunbjørn", "21.08", "15.10", "Jakttid på bjørn er hjelmet i \"Forskrift om forvaltning av rovvilt\". " +
                "Den som skal jakte bjørn må registrere seg som lisensjeger hos jegerregisteret. Det er den regionale rovviltnemnda som fastsetter " +
                "omfanget av lisensfelling, og definerer tidsrom innen jakttiden samt geografisk område ", "BrunBjørn.jpg", false));
            context.Animals.Add(new Animal("Brunnakke", "21.08", "23.12", "Den fire jakten på hav og fjord, jf viltloven §32, fra svenskegrensen til og med Vest-Agder fylke: 10. september - 23.desember", "Brunnakke.jpg", false));
            context.Animals.Add(new Animal("Dåhjort", "25.09", "23,12", "Ingen spesielle bestemmelser", "Dåhjort.jpg", true));
            context.Animals.Add(new Animal("Elg", "03.11", "15.04", "Ingen spesielle bestemmelser", "Elg.jpg", true));
            context.Animals.Add(new Animal("Rådyr", "01.11", "05.10", "Ingen spesielle bestemmelser", "Hjort.jpg", true));
            context.Animals.Add(new Animal("Hjort", "02.12", "10.07", "Ingen spesielle bestemmelser", "Rådyr.jpg", true));
            context.Animals.Add(new Animal("Villsvin", "19.10", "23.12", "Ingen spesielle bestemmelser", "Villsvin.jpg", true));



            var winchester = (new Weapon() { WeaponName = "Winchester", Caliber="500" });
           
            context.Weapons.Add(winchester);

            var marcus = (new Hunter() { HunterName = "Marcus", Weapon = winchester});
            context.Hunters.Add(marcus);


            base.Seed(context);
        }
    }
}
