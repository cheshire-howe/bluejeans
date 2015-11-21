package blackboard.plugin.virtualclassroom.model;

import com.fasterxml.jackson.annotation.JsonInclude;

@JsonInclude(JsonInclude.Include.NON_NULL)
public class Next {
	private Long start;
	private Long end;

	/**
	* 
	* @return
	* The start
	*/
	public Long getStart() {
	return start;
	}

	/**
	* 
	* @param start
	* The start
	*/
	public void setStart(Long start) {
	this.start = start;
	}

	/**
	* 
	* @return
	* The end
	*/
	public Long getEnd() {
	return end;
	}

	/**
	* 
	* @param end
	* The end
	*/
	public void setEnd(Long end) {
	this.end = end;
	}
}
