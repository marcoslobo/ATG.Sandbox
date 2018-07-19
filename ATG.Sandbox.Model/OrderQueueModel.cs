using System;
using System.ComponentModel.DataAnnotations;

namespace ATG.Sandbox.Model
{
    public class OrderQueueModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe se deseja uma Compra ou venda!")]
        public string Side { get; set; }
        [Required]

        [Range(1, Int64.MaxValue, ErrorMessage = "A quantidade necessita ser um número inteiro positivo!")]
        public int Quantity { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o Símbolo!")]
        public string Symbol { get; set; }

        [Range(0.01, 99999, ErrorMessage = "O Preço precisa ser positivo!")]
        public decimal Price { get; set; }
    }
}
