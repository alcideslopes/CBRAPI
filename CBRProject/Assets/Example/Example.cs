using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Example : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// Instaciacao da biblioteca de CBR
		CBRAPI cbrAPI = new CBRAPI("Teste.json");

		// Instanciacao do novo caso da base de casos
		Case caso = new Case();

		// Estruturacao da descricao do problema do caso
		caso.caseDescription.Add(new CaseFeature(0, "Atributo0", typeof(int), 1));
		caso.caseDescription.Add(new CaseFeature(1, "Atributo1", typeof(string), "Tipo1"));
		caso.caseDescription.Add(new CaseFeature(2, "Atributo2", typeof(float), 2.5f));
		caso.caseDescription.Add(new CaseFeature(3, "Atributo3", typeof(bool), true));

		// Estruturacao da descricao da solucao do caso
		caso.caseSolution.Add(new CaseFeature(0, "Atributo0", typeof(int), 5));
		caso.caseSolution.Add(new CaseFeature(1, "Atributo1", typeof(string), "Tipo5"));

		// Adicionando um caso na base de casos
		cbrAPI.AddCase(caso);

		// Instanciacao da estrutura do caso
		ConsultStructure consultStructure = new ConsultStructure();

		// Informando qual medida de similaridade global utilizar
		consultStructure.globalSimilarity = new EuclideanDistance(consultStructure);

		// Estruturacao de como o caso sera consultado na base de casos
		consultStructure.consultParams.Add(new ConsultParams(new List<int> { 0 }, 1f, new LinearFunction(0, 10)));
		consultStructure.consultParams.Add(new ConsultParams(new List<int> { 1 }, 1f, new Equals()));
		consultStructure.consultParams.Add(new ConsultParams(new List<int> { 2 }, 1f, new LinearFunction(0, 10)));
		consultStructure.consultParams.Add(new ConsultParams(new List<int> { 3 }, 1f, new Equals()));

		// Realizando uma consulta na base de casos
		List<Result> results = cbrAPI.Retrieve(caso, consultStructure);

		// Exibindo o resultado da consulta
		Debug.Log("Case " + results[0].id + ": " + results[0].matchPercentage);
	}

}
