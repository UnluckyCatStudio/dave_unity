using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graficos : MonoBehaviour {

	public void GraficosBajos (){
		QualitySettings.currentLevel = QualityLevel.Fastest; 
	}
	public void GraficosMedio (){
		QualitySettings.currentLevel = QualityLevel.Simple; 
	}
	public void GraficosAlto (){
		QualitySettings.currentLevel = QualityLevel.Beautiful; 
	}
	public void GraficosUltra (){
		QualitySettings.currentLevel = QualityLevel.Fantastic; 
	}

}
