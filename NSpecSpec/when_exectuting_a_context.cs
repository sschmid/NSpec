using NSpec.Domain;
using NSpec.Extensions;
using NSpec.Interpreter.Indexer;

namespace NSpecSpec
{
    public class when_executing_a_context : spec
    {
        private Context context;
        
        public void a_context_with_a_before_all()
        {
            //couldn't use before all when testing before all.
            //so sandboxed the subject (context) of this test
            //and executed the before all arrangement inline (not in before block)
            var context = new Context("test");

            var beforeAllCount = 0;

            context.Before = () => beforeAllCount++;

            context.BeforeFrequency = "all";

            base.context["the Befores run the first time"] = () =>
            {
                context.Befores();

                specify(() => beforeAllCount.should_be(1));

                base.context["the Befores run the second time"] = () =>
                {
                    context.Befores();

                    specify(() => beforeAllCount.should_be(1));
                };
            };
        }

        public void a_context()
        {
            before.each = () => context = new Context("test");

            describe["no examples of its own, but a subcontext with an example"] = () =>
            {
                before.each = () =>
                {
                    var sub = new Context("subContext");

                    sub.AddExample(new Example("example"));

                    context.AddContext(sub);
                };

                specify(() => context.ToString().is_not_null_or_empty());
            };

        }
    }
}