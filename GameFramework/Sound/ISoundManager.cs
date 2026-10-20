//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.Sound
{
    /// <summary>
    /// Sound manager interface.
    /// </summary>
    public interface ISoundManager
    {
        /// <summary>
        /// Gets the count of sound groups.
        /// </summary>
        int SoundGroupCount
        {
            get;
        }

        /// <summary>
        /// Play sound success event.
        /// </summary>
        event EventHandler<PlaySoundSuccessEventArgs> PlaySoundSuccess;

        /// <summary>
        /// Play sound failure event.
        /// </summary>
        event EventHandler<PlaySoundFailureEventArgs> PlaySoundFailure;

        /// <summary>
        /// Play sound update event.
        /// </summary>
        event EventHandler<PlaySoundUpdateEventArgs> PlaySoundUpdate;

        /// <summary>
        /// Play sound dependency asset event.
        /// </summary>
        event EventHandler<PlaySoundDependencyAssetEventArgs> PlaySoundDependencyAsset;

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        void SetResourceManager(IResourceManager resourceManager);

        /// <summary>
        /// Sets the sound helper.
        /// </summary>
        /// <param name="soundHelper">The sound helper.</param>
        void SetSoundHelper(ISoundHelper soundHelper);

        /// <summary>
        /// Checks whether the sound group exists.
        /// </summary>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <returns>Whether the sound group exists.</returns>
        bool HasSoundGroup(string soundGroupName);

        /// <summary>
        /// Gets a sound group.
        /// </summary>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <returns>The sound group to get.</returns>
        ISoundGroup GetSoundGroup(string soundGroupName);

        /// <summary>
        /// Gets all sound groups.
        /// </summary>
        /// <returns>All sound groups.</returns>
        ISoundGroup[] GetAllSoundGroups();

        /// <summary>
        /// Gets all sound groups.
        /// </summary>
        /// <param name="results">All sound groups.</param>
        void GetAllSoundGroups(List<ISoundGroup> results);

        /// <summary>
        /// Adds a sound group.
        /// </summary>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="soundGroupHelper">The sound group helper.</param>
        /// <returns>Whether the sound group was added successfully.</returns>
        bool AddSoundGroup(string soundGroupName, ISoundGroupHelper soundGroupHelper);

        /// <summary>
        /// Adds a sound group.
        /// </summary>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="soundGroupAvoidBeingReplacedBySamePriority">Whether the sounds in the sound group avoid being replaced by sounds of the same priority.</param>
        /// <param name="soundGroupMute">Whether the sound group is muted.</param>
        /// <param name="soundGroupVolume">The sound group volume.</param>
        /// <param name="soundGroupHelper">The sound group helper.</param>
        /// <returns>Whether the sound group was added successfully.</returns>
        bool AddSoundGroup(string soundGroupName, bool soundGroupAvoidBeingReplacedBySamePriority, bool soundGroupMute, float soundGroupVolume, ISoundGroupHelper soundGroupHelper);

        /// <summary>
        /// Adds a sound agent helper.
        /// </summary>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="soundAgentHelper">The sound agent helper to add.</param>
        void AddSoundAgentHelper(string soundGroupName, ISoundAgentHelper soundAgentHelper);

        /// <summary>
        /// Gets the serial IDs of all loading sounds.
        /// </summary>
        /// <returns>The serial IDs of all loading sounds.</returns>
        int[] GetAllLoadingSoundSerialIds();

        /// <summary>
        /// Gets the serial IDs of all loading sounds.
        /// </summary>
        /// <param name="results">The serial IDs of all loading sounds.</param>
        void GetAllLoadingSoundSerialIds(List<int> results);

        /// <summary>
        /// Checks whether the sound is loading.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound.</param>
        /// <returns>Whether the sound is loading.</returns>
        bool IsLoadingSound(int serialId);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <returns>The serial ID of the sound.</returns>
        int PlaySound(string soundAssetName, string soundGroupName);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="priority">The priority of loading the sound asset.</param>
        /// <returns>The serial ID of the sound.</returns>
        int PlaySound(string soundAssetName, string soundGroupName, int priority);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="playSoundParams">The play sound parameters.</param>
        /// <returns>The serial ID of the sound.</returns>
        int PlaySound(string soundAssetName, string soundGroupName, PlaySoundParams playSoundParams);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The serial ID of the sound.</returns>
        int PlaySound(string soundAssetName, string soundGroupName, object userData);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="priority">The priority of loading the sound asset.</param>
        /// <param name="playSoundParams">The play sound parameters.</param>
        /// <returns>The serial ID of the sound.</returns>
        int PlaySound(string soundAssetName, string soundGroupName, int priority, PlaySoundParams playSoundParams);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="priority">The priority of loading the sound asset.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The serial ID of the sound.</returns>
        int PlaySound(string soundAssetName, string soundGroupName, int priority, object userData);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="playSoundParams">The play sound parameters.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The serial ID of the sound.</returns>
        int PlaySound(string soundAssetName, string soundGroupName, PlaySoundParams playSoundParams, object userData);

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="priority">The priority of loading the sound asset.</param>
        /// <param name="playSoundParams">The play sound parameters.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The serial ID of the sound.</returns>
        int PlaySound(string soundAssetName, string soundGroupName, int priority, PlaySoundParams playSoundParams, object userData);

        /// <summary>
        /// Stops playing the sound.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound to stop.</param>
        /// <returns>Whether the sound was stopped successfully.</returns>
        bool StopSound(int serialId);

        /// <summary>
        /// Stops playing the sound.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound to stop.</param>
        /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
        /// <returns>Whether the sound was stopped successfully.</returns>
        bool StopSound(int serialId, float fadeOutSeconds);

        /// <summary>
        /// Stops all loaded sounds.
        /// </summary>
        void StopAllLoadedSounds();

        /// <summary>
        /// Stops all loaded sounds.
        /// </summary>
        /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
        void StopAllLoadedSounds(float fadeOutSeconds);

        /// <summary>
        /// Stops all loading sounds.
        /// </summary>
        void StopAllLoadingSounds();

        /// <summary>
        /// Pauses playing the sound.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound to pause.</param>
        void PauseSound(int serialId);

        /// <summary>
        /// Pauses playing the sound.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound to pause.</param>
        /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
        void PauseSound(int serialId, float fadeOutSeconds);

        /// <summary>
        /// Resumes playing the sound.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound to resume.</param>
        void ResumeSound(int serialId);

        /// <summary>
        /// Resumes playing the sound.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound to resume.</param>
        /// <param name="fadeInSeconds">The sound fade in time in seconds.</param>
        void ResumeSound(int serialId, float fadeInSeconds);
    }
}
