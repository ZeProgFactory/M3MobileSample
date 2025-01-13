namespace ScannerM3;

public class M3Barcode
{
   private string _Value;
   private string _Type;

   public M3Barcode(string value, string type)
   {
      _Value = value;
      _Type = type;
   }

   public string Value { get => _Value; set => _Value = value; }
   public string Type { get => _Type; set => _Type = value; }

   // - - -  - - - 

   public override string ToString()
   {
      return $"{_Value} ({_Type})";
   }
}
