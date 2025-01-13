using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace ScannerM3;

public enum SoundMode
{
   None = 0,
   Beep = 1,
   DingDong = 2
}

public enum ReadMode
{
   Async = 0,
   Sync = 1,
   Continuous = 2
}

public enum EndCharacter
{
   Enter = 0,
   Space = 1,
   Tab = 2,
   KeyboardEnter = 3,
   KeyboardSpace = 4,
   KeyboardTab = 5,
   None = 6
}

public enum OutputMode
{
   CopyPaste = 0,
   KeyboardEmulation = 1,
   NoneButClipboardCopy = 2
}

public delegate void OnBarcodeEventHandler(object sender, M3Barcode barcode);

public interface IM3Scanner
{
   public OnBarcodeEventHandler OnBarcode { get; set; }

   /// <summary>
   /// Action start decoding
   /// </summary>
   void DecodeStart();

   /// <summary>
   /// Action stop decoding
   /// </summary>
   void DecodeStop();

   /// <summary>
   /// Action to receive decoding result and setting parameter result
   /// </summary>
   void RegisterReceiver();

   /// <summary>
   /// 
   /// </summary>
   void UnregisterReceiver();

   /// <summary>
   /// Action Scanner Enable or Disable
   /// </summary>
   /// <param name="isEnabled"></param>
   void SetEnable(bool isEnabled);

   /// <summary>
   /// Action Scanner getting Parameter and Value
   /// </summary>
   /// <param name="param"></param>
   void GetScanParam(int param);

   /// <summary>
   /// Action Scanner setting Parameter and Value
   /// </summary>
   /// <param name="param"></param>
   void SetScanParam(int param, int value);

   /// <summary>
   /// 
   /// </summary>
   /// <param name="isEnabled"></param>
   void SetKeyDisable(bool isEnabled);


   void SetSound(SoundMode soundMode);
   void VibrationEnable(bool vibration);
   void SetReadMode(ReadMode readMode);
   void SetEndCharacter(EndCharacter endCharacter);
   void SetOutputMode(OutputMode outputMode);

}
