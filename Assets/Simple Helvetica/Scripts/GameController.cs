using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public string recHangman = "HANGMAN";

	public GameObject slovaResenja;

	public GameObject mestoNaKojeIduSlova;

	public float razmakIzmedjuSlova = 1.5f;

	private string[] _words = {
		"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
	};

	private string[] slovaRandum = new string[20];

	public GameObject slovoAzbuke;

	public GameObject mestoZaAzbuku;

	public float razmakTastature;

	int _brojPogodjenihSlogova = 0;
	int _brojPromasaja = 0;

	public GameObject[] deloviHangmana;

	bool isPlaying = true;

	// Use this for initialization
	void Start ()
	{

		for (int i = 0; i < deloviHangmana.Length; i++) {
			deloviHangmana [i].SetActive (false);

		} 

		IstancirajMestoZaHangmana ();
		GenerisatiSlova ();
		UbacitiAbecedu ();
      

	}

	public void IstancirajMestoZaHangmana ()
	{
		for (int i = 0; i < recHangman.Length; i++) {

			GameObject gameObjectInstancirani = Instantiate (slovaResenja, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity);
			gameObjectInstancirani.transform.SetParent (mestoNaKojeIduSlova.transform);
			gameObjectInstancirani.transform.localPosition = new Vector3 (i * razmakIzmedjuSlova, 0.0f, 0.0f);
			gameObjectInstancirani.transform.GetChild (0).GetComponent<SimpleHelvetica> ().Text = recHangman [i].ToString ();
			gameObjectInstancirani.transform.GetChild (0).GetComponent<SimpleHelvetica> ().GenerateText ();
			gameObjectInstancirani.transform.GetChild (0).gameObject.SetActive (false);
		}

	}

	public void GenerisatiSlova ()
	{

		for (int i = 0; i < recHangman.Length; i++) {
			slovaRandum [i] = recHangman [i].ToString ();
		}
		for (int i = recHangman.Length; i < slovaRandum.Length; i++) {
			slovaRandum [i] = _words [Random.Range (0, 26)];
		}

		for (int i = 0; i < slovaRandum.Length; i++) {
			string tmp = slovaRandum [i];

			int j = Random.Range (0, slovaRandum.Length);

			slovaRandum [i] = slovaRandum [j];

			slovaRandum [j] = tmp;
		}
	}

	public void UbacitiAbecedu ()
	{
		for (int i = 0; i < slovaRandum.Length; i++) {
			GameObject gameObjectInstancirani = Instantiate (slovoAzbuke, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity);
			if (i < 10)
				gameObjectInstancirani.transform.SetParent (mestoZaAzbuku.transform.GetChild (0));
			else
				gameObjectInstancirani.transform.SetParent (mestoZaAzbuku.transform.GetChild (1));
			gameObjectInstancirani.transform.localPosition = new Vector3 ((i * razmakTastature) % (razmakTastature * 10), 0.0f, 0.0f);
			gameObjectInstancirani.transform.GetChild (0).GetComponent<SimpleHelvetica> ().Text = slovaRandum [i].ToString ();
			gameObjectInstancirani.transform.GetChild (0).GetComponent<SimpleHelvetica> ().GenerateText ();
            

		}
	}
		
	public void Update ()
	{
		if (isPlaying) {
			if (Input.GetMouseButtonUp (0)) {
				RaycastHit hit = new RaycastHit ();
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {
					Debug.Log (hit.transform.GetChild (0).GetComponent<SimpleHelvetica> ().Text);
					ProveriSlovo (hit.transform.gameObject);
				}
			}
		}
	}

	public void ProveriSlovo (GameObject slovo)
	{
		bool tacno = false;

		for (int i = 0; i < recHangman.Length; i++) {
			if (slovo.transform.GetChild (0).GetComponent<SimpleHelvetica> ().Text == recHangman [i].ToString ()) {
				mestoNaKojeIduSlova.transform.GetChild (i).GetChild (0).gameObject.SetActive (true);
				mestoNaKojeIduSlova.transform.GetChild (i).GetChild (0).GetComponent<SimpleHelvetica> ().Text = slovo.transform.GetChild (0).GetComponent<SimpleHelvetica> ().Text;
				tacno = true;
				slovo.transform.transform.gameObject.SetActive (false);
				_brojPogodjenihSlogova++;
			}
		}
		if (!tacno) {
			if (_brojPromasaja < 6) {
				deloviHangmana [_brojPromasaja].SetActive (true);
				_brojPromasaja++;
			}
               
		}
		if (_brojPromasaja == 6) {
			Debug.Log ("Obesi se!");

			isPlaying = !isPlaying;
		}
		if (_brojPogodjenihSlogova == recHangman.Length) {
			Debug.Log ("Pobeda");
			isPlaying = !isPlaying;
		}
	}


	public void ProveriSlovo2 (GameObject slovo)
	{
		bool tacno = false;

		for (int i = 0; i < recHangman.Length; i++) {
			if (slovo.transform.GetChild (0).GetComponent<SimpleHelvetica> ().Text == recHangman [i].ToString ()) {
				mestoNaKojeIduSlova.transform.GetChild (0).gameObject.SetActive (true);
				mestoNaKojeIduSlova.transform.GetChild (0).GetComponent<SimpleHelvetica> ().Text = slovo.transform.GetChild (0).GetComponent<SimpleHelvetica> ().Text;
				tacno = true;
				slovo.transform.transform.gameObject.SetActive (false);
				_brojPogodjenihSlogova++;
			}
		}
		if (!tacno) {
			if ((_brojPromasaja < 6) || (_brojPromasaja < 0)) { 
				deloviHangmana [_brojPromasaja++].SetActive (true);
             
			}

		}
		if (_brojPromasaja == 6) {
			Debug.Log ("Obesi se!");

			isPlaying = !isPlaying;
		}
		if (_brojPogodjenihSlogova == recHangman.Length) {
			Debug.Log ("Pobeda");
			isPlaying = !isPlaying;
		}
	}

}
	

