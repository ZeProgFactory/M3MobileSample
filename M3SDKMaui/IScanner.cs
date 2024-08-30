using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace M3SDKMaui;

public interface IM3Scanner
{
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
}
