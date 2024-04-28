using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace crudAPI.Domain.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é um endereço de email válido.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "O Email deve ter entre 10 e 100 caracteres.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome deve ter entre 2 e 100 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        public DateOnly DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo LinkedIn é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O LinkedIn deve ter entre 2 e 100 caracteres.")]
        public string LinkedIn { get; set; } = null!;

        [Required(ErrorMessage = "O campo Instagram é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O Instagram deve ter entre 2 e 100 caracteres.")]
        public string Instagram { get; set; } = null!;

        public string? Profissao { get; set; }
    }

    internal class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
       private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}
    }

