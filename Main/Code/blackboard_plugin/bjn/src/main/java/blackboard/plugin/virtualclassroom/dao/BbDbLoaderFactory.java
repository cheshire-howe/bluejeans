package blackboard.plugin.virtualclassroom.dao;

import blackboard.persist.PersistenceException;
import blackboard.persist.course.CourseDbLoader;
import blackboard.persist.course.CourseMembershipDbLoader;
import blackboard.persist.user.UserDbLoader;
import blackboard.platform.config.ConfigurationService;
import blackboard.platform.config.ConfigurationServiceFactory;
import blackboard.platform.gradebook2.impl.GradableItemDAO;
import blackboard.platform.gradebook2.impl.GradeDetailDAO;
import blackboard.platform.gradebook2.impl.GradingSchemaDAO;
import blackboard.platform.intl.BundleManagerEx;
import blackboard.platform.intl.BundleManagerExFactory;
import blackboard.platform.security.persist.CourseRoleDbLoader;

public class BbDbLoaderFactory {

    public static UserDbLoader getUserDbLoader() throws PersistenceException {
        return UserDbLoader.Default.getInstance();
    }

    public static CourseMembershipDbLoader getCourseMembershipDbLoader() throws PersistenceException {
        return CourseMembershipDbLoader.Default.getInstance();
    }

    public static CourseDbLoader getCourseDbLoader() throws PersistenceException {
        return CourseDbLoader.Default.getInstance();
    }

    public static ConfigurationService getBbConfigurationService() throws PersistenceException {
        return ConfigurationServiceFactory.getInstance();
    }

    public static CourseRoleDbLoader getCourseRoleDbLoader() throws PersistenceException {
        return CourseRoleDbLoader.Default.getInstance();
    }

    public static BundleManagerEx getBundleManagerEx() {
        return BundleManagerExFactory.getInstance();
    }

    public static GradableItemDAO getGradableItemDAO() {
        return GradableItemDAO.get();
    }

    public static GradingSchemaDAO getGradingSchemaDAO() {
        return GradingSchemaDAO.get();
    }

    public static GradeDetailDAO getGradeDetailDAO() {
        return GradeDetailDAO.get();
    }
}