using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolgozok
{
	internal class Dolgozo
	{

		public string Nev { get; set; }
		public string Beosztas { get; set; }
		public string Email { get; set; }
		public string TelefonSzam { get; set; }
		public int Fizetes { get; set; }
		public string Nem { get; set; }
		
		public Dolgozo(string nev, string beosztas, string email, string telefonSzam, int fizetes, string nem)
		{
			Nev = nev;
			Beosztas = beosztas;
			Email = email;
			TelefonSzam = telefonSzam;
			Fizetes = fizetes;
			Nem = nem;
		}
	}
}
