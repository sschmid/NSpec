using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace NSpec.Tests.WhenRunningSpecs.BeforeAndAfter
{
    [TestFixture]
    [Category("Async")]
    public class async_when_there_are_no_specs : when_running_specs
    {
        [SetUp]
        public void setup()
        {
            sequence_spec.sequence = "";
        }

        class async_before_all_example_spec : sequence_spec
        {
            async Task before_all()
            {
                await Task.Run(() => sequence = "executed");
            }
        }

        [Test]
        public void async_before_all_is_not_executed()
        {
            Run(typeof(async_before_all_example_spec));

            sequence_spec.sequence.Should().Be("");
        }

        class async_before_each_example_spec : sequence_spec
        {
            async Task before_each()
            {
                await Task.Run(() => sequence = "executed");
            }
        }

        [Test]
        public void async_before_each_is_not_executed()
        {
            Run(typeof(async_before_each_example_spec));

            sequence_spec.sequence.Should().Be("");
        }

        class after_each_example_spec : sequence_spec
        {
            async Task after_each()
            {
                await Task.Run(() => sequence += "executed");
            }
        }

        [Test]
        public void after_each_is_not_executed()
        {
            Run(typeof (after_each_example_spec));

            sequence_spec.sequence.Should().Be("");
        }

        class after_all_example_spec : sequence_spec
        {
            async Task after_all()
            {
                await Task.Run(() => sequence += "executed");
            }
        }

        [Test]
        public void after_all_is_not_executed()
        {
            Run(typeof (after_all_example_spec));

            sequence_spec.sequence.Should().Be("");
        }
    }
}