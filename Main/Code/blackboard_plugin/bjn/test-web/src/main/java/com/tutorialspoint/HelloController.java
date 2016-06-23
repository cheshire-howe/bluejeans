package com.tutorialspoint;

import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.client.RestTemplate;
import org.springframework.web.servlet.ModelAndView;

@Controller
public class HelloController {
	
	@RequestMapping(value = "/", method = RequestMethod.GET)
	public ModelAndView classes() {
		RestTemplate restTemplate = new RestTemplate();
		Meeting cls = restTemplate.getForObject("http://10.42.0.44:8112/api/Meeting/2344948", Meeting.class);
		
		ModelAndView mView = new ModelAndView("classes");
		mView.addObject("cls", cls);
		
		return mView;
	}
	
	@RequestMapping(value = "/create", method = RequestMethod.GET)
	public ModelAndView getCreateClass(@ModelAttribute Meeting meeting, ModelMap model) {
		return new ModelAndView("create", "command", new Meeting());
	}
	
	@RequestMapping(value = "/create", method = RequestMethod.POST)
	public String postClass(@ModelAttribute Meeting meeting) {
		return "result";
	}
	
	@RequestMapping(value = "/test")
	public ModelAndView getTest() {
		return new ModelAndView("test/test");
	}
}