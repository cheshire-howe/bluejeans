package blackboard.plugin.virtualclassroom.spring.web;

import javax.servlet.http.HttpServletRequest;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.servlet.ModelAndView;

import com.spvsoftwareproducts.blackboard.utils.B2Context;

@Controller
@RequestMapping("/module")
public class ModuleController {
	
	// GET: /module/view
	@RequestMapping(value = "/view", method = RequestMethod.GET)
	public ModelAndView getClasses(HttpServletRequest request, @RequestParam(value="course_id", defaultValue="") String bbCourseId) {

		B2Context b2Context = new B2Context(request);
		ModelAndView mView = new ModelAndView("module/view");
		mView.addObject("webapp", b2Context.getPath());

		return mView;
	}
}