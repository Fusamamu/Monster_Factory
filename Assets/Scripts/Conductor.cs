using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
	public float SongBpm;

	public float SecPerBeat;
	public float SongPosition;

	public float SongPositionInBeats;
	public float DspSongTime;

	public AudioSource MusicSource;
	
	//keep all the position-in-beats of notes in the song
	public float[] Beats;

	int nextIndex = 0;

	private int beatsShownInAdvance = 3;
	
	
	
	//the number of beats in each loop
	public float BeatsPerLoop;

	//the total number of loops completed since the looping clip first started
	public int CompletedLoops = 0;

	//The current position of the song within the loop in beats.
	public float LoopPositionInBeats;
	
	
	
	
	
	
	
	
	

	[SerializeField] private GameObject Note;

	public class NoteData
	{
		public GameObject Note;
		public float Beat;
	}

	public List<NoteData> Notes = new List<NoteData>();
	public List<NoteData> PendingNotes = new List<NoteData>();

	public Transform SpawnPos;
	public Transform RemovePos;

	private bool isStart;
	
	private void Start()
	{
		StartCoroutine(Wait());
	}

	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(5);
		
		SecPerBeat  = 60f / SongBpm;
		DspSongTime = (float)AudioSettings.dspTime;

		MusicSource.Play();

		isStart = true;
	}
	
	void Update()
	{
		if(!isStart)
			return;
		
		SongPosition = (float)(AudioSettings.dspTime - DspSongTime);

		SongPositionInBeats = SongPosition / SecPerBeat;
		
		
		
		if (nextIndex < Beats.Length && Beats[nextIndex] < SongPositionInBeats + beatsShownInAdvance)
		{
			var _note = Instantiate(Note, SpawnPos.transform.position, Quaternion.identity);


			var _noteData = new NoteData()
			{
				Note = _note,
				Beat = Beats[nextIndex]
			};
			
			Notes.Add(_noteData);

			nextIndex++;

			// if (nextIndex > Beats.Length - 1)
			// {
			// 	nextIndex = 0;
			// 	MusicSource.Stop();
			// 	DspSongTime = (float)AudioSettings.dspTime;
			// 	MusicSource.Play();
			// }
		}
		
		// if (SongPositionInBeats >= (CompletedLoops + 1) * BeatsPerLoop)
		// 	CompletedLoops++;
		//
		// LoopPositionInBeats = SongPositionInBeats - CompletedLoops * BeatsPerLoop;

		foreach (var _note in Notes)
		{
			_note.Note.transform.position = Vector3.Lerp(
				SpawnPos.transform.position,
				RemovePos.transform.position,
				(beatsShownInAdvance - (_note.Beat - SongPositionInBeats)) / beatsShownInAdvance
			);

			if (Vector3.Distance(RemovePos.transform.position, _note.Note.transform.position) < float.Epsilon)
			{
				PendingNotes.Add(_note);
			}
		}

		foreach (var _note in PendingNotes)
		{
			Notes.Remove(_note);
			Destroy(_note.Note);
		}
	}

	// private void OnAudioFilterRead(float[] data, int channels)
	// {
	// 	
	// }
}
