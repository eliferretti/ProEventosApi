using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Aplication.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime DataEvento { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")] 
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Intervalo permitido entre 3 e 50 caracteres.")]
        public string Tema { get; set; }
        [Display(Name = "Qtd Pessoas")]
        [Range(1, 120000, ErrorMessage = "{0} não pode ser menor que 1 e maior que 120.000.")]
        public int QtdPessoas { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage ="Não é uma imagem válida. (gif, jpg, jpeg, bmp, png)")]
        public string ImagemUrl { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")] 
        [Phone(ErrorMessage = "O campo {0} está com número inválido")]
        public string Telefone { get; set; }
        [Display(Name = "e-mail")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage ="O Campo {0} precisa ser um e-mail válido.")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}

// {
//   "id": 6,
//   "local": "t2 Leblon - Rio de janeiro",
//   "dataEvento": "20/01/2023 10:30:20",
//   "tema": "t Recode Pro 2023",
//   "qtdPessoas": 2,
//   "imagemUrl": "trecodepro.jpg",
//   "telefone": "119u83323845",
//   "email": "trecorerecode.org.br",
//   "lotes": [],
//   "redesSociais": [],
//   "palestrantes": null
// }