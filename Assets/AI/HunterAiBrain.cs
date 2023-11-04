using UnityEngine;
using FMODUnity;

//public class HunterAiBrain : MonoBehaviour
//{
    //public EventReference fmodEvent; 
   // private FMOD.Studio.EventInstance eventInstance;
  //  private FMOD.Studio.Bus bus; 
 //   private FMOD.DSP dspEffect;
  //  private float[] spectrumData;

//    private void Start()
  //  {
     
    //    eventInstance = RuntimeManager.CreateInstance(fmodEvent);
    //    eventInstance.start();

       
   //     bus = RuntimeManager.GetBus("bus:/");
    //    bus.getDSP(0, out dspEffect);
      //  dspEffect.setMeteringEnabled(true, true);

    //    int desiredSpectrumDataLength = 1024; 

       
      //  spectrumData = new float[desiredSpectrumDataLength];
  //  }

 //   private void Update()
  //  {
        // Correct the getSpectrum call to use a valid window type (e.g., HANNING).
    //    dspEffect.getSpectrum(spectrumData, 0, spectrumData.Length, FMOD.DSP_FFT_WINDOW.HANNING);
        
        // Use the spectrumData for your analysis or visualization.
        // You can access and process the frequency data in the spectrumData array.
 //   }
//}