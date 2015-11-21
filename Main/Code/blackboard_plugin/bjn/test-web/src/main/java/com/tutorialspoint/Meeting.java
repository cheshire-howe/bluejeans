package com.tutorialspoint;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Meeting {
	//private int id;
	private String title;
	private String description;
	
	/*public int getId() {
		return id;
	}*/

	public String getTitle() {
		return title;
	}

	public String getDescription() {
		return description;
	}
}