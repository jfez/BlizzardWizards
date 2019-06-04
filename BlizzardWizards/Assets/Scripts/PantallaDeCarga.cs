using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PantallaDeCarga : MonoBehaviour
{

    public static PantallaDeCarga Instancia { get; private set; }

    public Image imageDeCarga;
    [Range(0.01f, 1.0f)]
    public float velocidadAparecer = 0.5f;
    [Range(0.01f, 1.0f)]
    public float velocidadOcultar = 0.5f;
    //public Slider barra;
    //private AsyncOperation asyn;

    void Awake()
    {
        DefinirSingleton();
    }

    private void DefinirSingleton()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(this);
            imageDeCarga.gameObject.SetActive(false);
            //barra.gameObject.SetActive(false);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CargarEscena(string nombreEscena)
    {
        StartCoroutine(MostrarPantallaDeCarga(nombreEscena));
    }


    private IEnumerator MostrarPantallaDeCarga(string nombreEscena)
    {
        //barra.value = 0;
        imageDeCarga.gameObject.SetActive(true);
        //barra.gameObject.SetActive(true);
        Color c = imageDeCarga.color;
        c.a = 0.0f;
        

        //Mientras no esté totalmente visible va aumentando su visibilidad
        while (c.a < 1)
        {
            imageDeCarga.color = c;
            c.a += velocidadAparecer;
            yield return null;
        }

        //Carga la escena
        SceneManager.LoadScene(nombreEscena);
        //asyn = SceneManager.LoadSceneAsync(nombreEscena);


        //Espera a que haya cargado la nueva escena
        while (!nombreEscena.Equals(SceneManager.GetActiveScene().name))
        {
            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);

        /*while (!asyn.isDone)
        {
            barra.value = asyn.progress;
            yield return null;
        }*/

        //Mientras la imagen de carga siga visible va desvaneciéndola
        while (c.a > 0)
        {
            imageDeCarga.color = c;
            c.a -= velocidadOcultar;
            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);
        imageDeCarga.gameObject.SetActive(false);
        //barra.gameObject.SetActive(false);
    }
}
