using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AnonDrop.RecipientClient.Controllers
{
	public class SubmitController : ControllerBase
	{
		private readonly Random _rand = new Random();

		public IActionResult Submit(string link, string password)
		{
			if (string.IsNullOrWhiteSpace(link)
			 || string.IsNullOrWhiteSpace(password))
				return ValidationProblem();

			var filePath = Path.Combine(Environment.CurrentDirectory, "anondrop_data.json");

			var data = new string[][] { };

			if (System.IO.File.Exists(filePath))
			{
				var sr = new StreamReader(filePath);
				data = JsonSerializer.Deserialize<string[][]>(sr.ReadToEnd());
			}
			else
			{
				System.IO.File.CreateText(filePath);
			}

			data = data.Append(new[] {link, password}).ToArray();

			var shuffledEnumerable = data.Shuffle(_rand);
			var shuffledArray =
				(from item in shuffledEnumerable
				 select item.ToArray()).ToArray();
			var serialised = JsonSerializer.Serialize(shuffledArray);

			System.IO.File.WriteAllText(filePath, serialised);

			return Ok();
		}
	}
}