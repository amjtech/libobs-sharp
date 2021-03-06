﻿using System;

namespace OBS
{
    public class ObsOutput : IObsContextData, IDisposable
    {
        internal IntPtr instance; //pointer to unmanaged object

        public unsafe ObsOutput(ObsOutputType type, string id, string name)
        {
            instance = libobs.obs_output_create(/*(libobs.obs_source_type)type,*/ id, name, IntPtr.Zero, IntPtr.Zero);

            if (instance == null)
                throw new ApplicationException("obs_output_create failed");
        }

        public unsafe ObsOutput(ObsOutputType type, string id, string name, ObsData settings)
        {
            instance = libobs.obs_output_create(/*(libobs.obs_source_type)type,*/ id, name, settings.GetPointer(), IntPtr.Zero);

            if (instance == null)
                throw new ApplicationException("obs_output_create failed");
        }

        public unsafe ObsOutput(IntPtr instance)
        {
            this.instance = instance;
            libobs.obs_output_addref(instance);
        }

        public unsafe bool Start()
        {
            return libobs.obs_output_start(instance);
        }

		public unsafe void Stop()
		{
			libobs.obs_output_stop(instance);
		}

		public unsafe bool CanPause()
		{
			throw new NotImplementedException();
//			libobs.obs_output_can_pause(instance);
		}

		public unsafe void Pause()
		{
			throw new NotImplementedException();
//			libobs.obs_output_pause(instance);
		}

		public unsafe bool Resume()
		{
			throw new NotImplementedException();
//			return libobs.obs_output_start(instance);
		}

		public unsafe void Update(ObsData settings)
		{
			libobs.obs_output_update(instance, settings.GetPointer());
		}

		public unsafe IntPtr GetPointer()
        {
            return instance;
        }

        public unsafe void Dispose()
        {
            if (instance == IntPtr.Zero)
                return;

            libobs.obs_output_release(instance);

            instance = IntPtr.Zero;
        }

        public unsafe void SetVideoEncoder(ObsEncoder videoEncoder)
        {
            libobs.obs_output_set_video_encoder(instance, videoEncoder.GetPointer());
        }

        public unsafe void SetAudioEncoder(ObsEncoder audioEncoder)
        {
            libobs.obs_output_set_audio_encoder(instance, audioEncoder.GetPointer(), UIntPtr.Zero);
        }

	    public unsafe bool Active
	    {
			get { return libobs.obs_output_active(instance); }
	    }
    }

    public enum ObsOutputType : int
    {
        Dummy
    };
}
