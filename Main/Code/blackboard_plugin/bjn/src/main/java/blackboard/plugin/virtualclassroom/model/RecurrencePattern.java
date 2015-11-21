package blackboard.plugin.virtualclassroom.model;

//import com.fasterxml.jackson.annotation.JsonInclude;

//@JsonInclude(JsonInclude.Include.NON_NULL)
public class RecurrencePattern {

	private String recurrenceType;
	private Object endDate;
	private Integer recurrenceCount;
	private Integer frequency;
	private Integer daysOfWeekMask;
	private Integer dayOfMonth;
	private String weekOfMonth;
	private String monthOfYear;

	/**
	 * 
	 * @return The recurrenceType
	 */
	public String getRecurrenceType() {
		return recurrenceType;
	}

	/**
	 * 
	 * @param recurrenceType
	 *            The recurrenceType
	 */
	public void setRecurrenceType(String recurrenceType) {
		this.recurrenceType = recurrenceType;
	}

	/**
	 * 
	 * @return The endDate
	 */
	public Object getEndDate() {
		return endDate;
	}

	/**
	 * 
	 * @param endDate
	 *            The endDate
	 */
	public void setEndDate(Object endDate) {
		this.endDate = endDate;
	}

	/**
	 * 
	 * @return The recurrenceCount
	 */
	public Integer getRecurrenceCount() {
		return recurrenceCount;
	}

	/**
	 * 
	 * @param recurrenceCount
	 *            The recurrenceCount
	 */
	public void setRecurrenceCount(Integer recurrenceCount) {
		this.recurrenceCount = recurrenceCount;
	}

	/**
	 * 
	 * @return The frequency
	 */
	public Integer getFrequency() {
		return frequency;
	}

	/**
	 * 
	 * @param frequency
	 *            The frequency
	 */
	public void setFrequency(Integer frequency) {
		this.frequency = frequency;
	}

	/**
	 * 
	 * @return The daysOfWeekMask
	 */
	public Integer getDaysOfWeekMask() {
		return daysOfWeekMask;
	}

	/**
	 * 
	 * @param daysOfWeekMask
	 *            The daysOfWeekMask
	 */
	public void setDaysOfWeekMask(Integer daysOfWeekMask) {
		this.daysOfWeekMask = daysOfWeekMask;
	}

	/**
	 * 
	 * @return The dayOfMonth
	 */
	public Integer getDayOfMonth() {
		return dayOfMonth;
	}

	/**
	 * 
	 * @param dayOfMonth
	 *            The dayOfMonth
	 */
	public void setDayOfMonth(Integer dayOfMonth) {
		this.dayOfMonth = dayOfMonth;
	}

	/**
	 * 
	 * @return The weekOfMonth
	 */
	public String getWeekOfMonth() {
		return weekOfMonth;
	}

	/**
	 * 
	 * @param weekOfMonth
	 *            The weekOfMonth
	 */
	public void setWeekOfMonth(String weekOfMonth) {
		this.weekOfMonth = weekOfMonth;
	}

	/**
	 * 
	 * @return The monthOfYear
	 */
	public String getMonthOfYear() {
		return monthOfYear;
	}

	/**
	 * 
	 * @param monthOfYear
	 *            The monthOfYear
	 */
	public void setMonthOfYear(String monthOfYear) {
		this.monthOfYear = monthOfYear;
	}

}