<#import "template.ftl" as layout>
<@layout.registeredLayout displayMessage=false; section>
    <#if section = "form">
        <div id="kc-info-message">
            <p class="text-neutral-700 mb-6 leading-relaxed">
                <#if messageHeader??>
                    <span class="font-semibold text-neutral-900 block mb-2">${messageHeader}</span>
                </#if>
                <#if requiredActions??>
                    <#list requiredActions as reqActionItem>
                        <span class="block mb-1">${kcSanitize(msg("requiredAction.${reqActionItem}"))?no_esc}</span>
                    </#list>
                <#else>
                    <span>${message.summary}</span>
                </#if>
            </p>

            <#if skipLink??>
            <#else>
                <#if pageRedirectUri?has_content>
                    <p class="mt-6">
                        <a
                            href="${pageRedirectUri}"
                            class="inline-block bg-primary-500 hover:bg-primary-600 text-white font-semibold py-2.5 px-6 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2"
                        >
                            ${kcSanitize(msg("backToApplication"))?no_esc}
                        </a>
                    </p>
                <#elseif actionUri?has_content>
                    <p class="mt-6">
                        <a
                            href="${actionUri}"
                            class="inline-block bg-primary-500 hover:bg-primary-600 text-white font-semibold py-2.5 px-6 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2"
                        >
                            ${kcSanitize(msg("proceedWithAction"))?no_esc}
                        </a>
                    </p>
                <#elseif (client.baseUrl)?has_content>
                    <p class="mt-6">
                        <a
                            href="${client.baseUrl}"
                            class="inline-block bg-primary-500 hover:bg-primary-600 text-white font-semibold py-2.5 px-6 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2"
                        >
                            ${kcSanitize(msg("backToApplication"))?no_esc}
                        </a>
                    </p>
                </#if>
            </#if>
        </div>
    </#if>
</@layout.registeredLayout>
